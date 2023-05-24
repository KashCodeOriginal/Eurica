﻿using UnityEngine;

namespace Services.PlaySounds
{
    public interface IPlaySoundsService
    {
        public void PlayAudioClip(AudioClip audioClip, VolumeLevel volume, bool canPlayMultiple = false, bool playOnlyOnce = true);
        public void ResetSoundStates();
        public void SetUp(AudioSource audioSource);
        public bool CanPlay(AudioClip audioClip, bool canPlayMultiple = false, bool playOnlyOnce = true);
        public void SetUpVolumeMultiplier(float volume);
    }
}