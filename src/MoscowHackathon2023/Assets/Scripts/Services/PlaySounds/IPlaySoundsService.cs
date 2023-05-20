using UnityEngine;

namespace Services.PlaySounds
{
    public interface IPlaySoundsService
    {
        public void PlayAudioClip(AudioClip audioClip, VolumeLevel volume, bool canPlayMultiple = false, bool playOnlyOnce = true);
        public void SetUp(AudioSource audioSource);
    }
}