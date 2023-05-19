using UnityEngine;

namespace Services.PlaySounds
{
    public interface IPlaySoundsService
    {
        public void PlayOneShot(AudioClip audioClip, float volume);
        public void SetUp(AudioSource audioSource);
    }
}