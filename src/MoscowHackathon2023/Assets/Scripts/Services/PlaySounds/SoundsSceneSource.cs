using UI.GameplayScreen;
using UnityEngine;

namespace Services.PlaySounds
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundsSceneSource : MonoBehaviour
    {
        private AudioSource _audioSource;
        private float _startVolume;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _startVolume = _audioSource.volume;

            if (GameplayScreen.Instance)
            {
                UpdateCurrentVolume(GameplayScreen.Instance.SettingsManager.GetVolume());
            }
            else
            {
                UpdateCurrentVolume(0f);
            }
        }

        public void UpdateCurrentVolume(float volume)
        {
            _audioSource.volume = _startVolume * volume;
        }
    }
}