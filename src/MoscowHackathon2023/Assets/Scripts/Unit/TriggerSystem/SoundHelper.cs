using Services.PlaySounds;
using UnityEngine;
using Zenject;

namespace Unit.TriggerSystem
{
    public class SoundHelper : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private float _volume;
        [SerializeField] private bool _canPlayMultiple;
        [SerializeField] private bool _playOnlyOnce;

        private IPlaySoundsService _playSoundsService;

        [Inject]
        public void Construct(IPlaySoundsService playSoundsService)
        {
            _playSoundsService = playSoundsService;
        }

        public void PlaySound(float afterTime = 0f)
        {
            Invoke(nameof(Play), afterTime);
        }

        private void Play()
        {
            _playSoundsService.PlayAudioClip(_audioClip, _volume, _canPlayMultiple, _playOnlyOnce);
        }
    }
}