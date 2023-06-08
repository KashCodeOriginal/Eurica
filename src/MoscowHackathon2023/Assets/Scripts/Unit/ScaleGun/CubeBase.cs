using UnityEngine;

namespace Unit.ScaleGun
{
    [RequireComponent(typeof(AudioSource))]
    public class CubeBase : MonoBehaviour
    {
        private const float minImpact = 0.5f;
        [SerializeField] private AudioClip[] _clipVariants;
        private AudioSource _audioSource;

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.impulse.magnitude > minImpact)
            {
                AudioClip clip = _clipVariants[Random.Range(0, _clipVariants.Length)];

                if (!_audioSource)
                    _audioSource = GetComponent<AudioSource>();

                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.PlayOneShot(clip);
            }
        }
    }
}
