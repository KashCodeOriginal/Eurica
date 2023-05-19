using UnityEngine;

namespace Services.PlaySounds
{
    public class PlaySoundsService : IPlaySoundsService
    {
        private AudioSource _audioSource;

        public void PlayOneShot(AudioClip audioClip, float volume)
        {
            _audioSource.PlayOneShot(audioClip);
        }

        public void SetUp(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }
    }
}