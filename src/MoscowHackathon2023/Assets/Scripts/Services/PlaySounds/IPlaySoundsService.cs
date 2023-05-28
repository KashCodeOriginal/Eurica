using UnityEngine;

namespace Services.PlaySounds
{
    public interface IPlaySoundsService
    {
        public void PlayAudioClip(AudioClip audioClip, float volume, 
            bool canPlayMultiple = false, bool playOnlyOnce = true, 
            float minPitch = 1f, float maxPitch = 1f);
        public void ResetSoundStates();
        public float GetVolumeLevel(VolumeLevel volume);
        public void SetUp(AudioSource audioSource);
        public bool CanPlay(AudioClip audioClip, bool canPlayMultiple = false, bool playOnlyOnce = true);
        public void SetUpVolumeMultiplier(float volume);
    }
}