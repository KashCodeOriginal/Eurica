using UnityEngine;

namespace Services.PlaySounds
{
    public interface IPlaySoundsService
    {
        public void PlayOneShot(AudioClip audioClip, VolumeLevel volume, bool canPlayMultiple = false);
        public void SetUp(AudioSource audioSource);
    }
}