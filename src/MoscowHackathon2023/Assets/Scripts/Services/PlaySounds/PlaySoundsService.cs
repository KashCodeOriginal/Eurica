using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.PlaySounds
{
    public class PlaySoundsService : IPlaySoundsService
    {
        private AudioSource _audioSource;
        private ICoroutineRunner _coroutineRunner;
        private Dictionary<AudioClip, bool> _soundStates;

        private float _currentVolumeMultiplier;

        public PlaySoundsService(ICoroutineRunner coroutineRunner)
        {
            _soundStates = new Dictionary<AudioClip, bool>();
            _coroutineRunner = coroutineRunner;
        }

        public void SetUpVolumeMultiplier(float volume)
        {
            _currentVolumeMultiplier = volume;
        }

        public void PlayAudioClip(AudioClip audioClip, VolumeLevel volume, bool canPlayMultiple = false, bool playOnlyOnce = true)
        {
            if (IsNotPlaying(audioClip, playOnlyOnce) || canPlayMultiple)
            {
                float volumeLevel = GetVolumeLevel(volume);
                _audioSource.PlayOneShot(audioClip, volumeLevel);
                _soundStates[audioClip] = true;

                _coroutineRunner.StartCoroutine(WaitForSoundFinish(audioClip.length, audioClip));
            }
        }

        public void ResetSoundStates()
        {
            _soundStates.Clear();
        }

        private bool IsNotPlaying(AudioClip audioClip, bool playOnlyOnce)
        {
            if (playOnlyOnce)
            {
                return !_soundStates.ContainsKey(audioClip);
            }
            else
            {
                return !_soundStates.ContainsKey(audioClip) || !_soundStates[audioClip];
            }
        }

        private IEnumerator WaitForSoundFinish(float soundLength, AudioClip audioClip)
        {
            yield return new WaitForSeconds(soundLength);
            _soundStates[audioClip] = false;
        }

        private float GetVolumeLevel(VolumeLevel volume)
        {
            var volumeLevel = _currentVolumeMultiplier * volume switch
            {
                VolumeLevel.Default => 1f,
                VolumeLevel.VoiceOver => 0.5f,
                VolumeLevel.StepsVolume => 0.25f,
                _ => 0f
            };
            return volumeLevel;
        }

        public void SetUp(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public bool CanPlay(AudioClip audioClip, bool canPlayMultiple = false, bool playOnlyOnce = true)
        {
            return (IsNotPlaying(audioClip, playOnlyOnce) || canPlayMultiple);
        }
    }
}