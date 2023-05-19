using UnityEngine;

namespace Services.PlaySounds
{
    public class PlaySoundsService : IPlaySoundsService
    {
        private AudioSource _audioSource;

        public void PlayOneShot(AudioClip audioClip, VolumeLevel volume)
        {
            var volumeLevel = volume switch
            {
                VolumeLevel.Default => 1f,
                VolumeLevel.VoiceOver => 0.5f,
                VolumeLevel.StepsVolume => 0.3f,
                _ => 0f
            };

            _audioSource.PlayOneShot(audioClip, volumeLevel);
        }

        public void SetUp(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}