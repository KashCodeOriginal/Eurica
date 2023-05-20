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

        public PlaySoundsService(ICoroutineRunner coroutineRunner)
        {
            _soundStates = new Dictionary<AudioClip, bool>();
            _coroutineRunner = coroutineRunner;
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

        private static float GetVolumeLevel(VolumeLevel volume)
        {
            float OVERALL_VOLUME = 0.3f; // TODO: Add to settings.

            var volumeLevel = OVERALL_VOLUME * volume switch
            {
                VolumeLevel.Default => 1f,
                VolumeLevel.VoiceOver => 0.5f,
                VolumeLevel.StepsVolume => 0.3f,
                _ => 0f
            };
            return volumeLevel;
        }

        public void SetUp(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}