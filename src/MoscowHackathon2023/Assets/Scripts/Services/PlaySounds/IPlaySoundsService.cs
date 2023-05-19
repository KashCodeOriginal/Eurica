using UnityEngine;

namespace Services.PlaySounds
{
    public interface IPlaySoundsService
    {
        public void PlayOneShot(AudioClip audioClip, VolumeLevel volume);
        public void SetUp(AudioSource audioSource);
    }
}