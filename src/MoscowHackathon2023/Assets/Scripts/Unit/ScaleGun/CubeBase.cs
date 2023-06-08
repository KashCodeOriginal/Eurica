using System.Collections;
using Unit.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.ScaleGun
{
    [RequireComponent(typeof(AudioSource))]
    public class CubeBase : MonoBehaviour
    {
        public UnityAction RequestDetach;
        private const float minImpact = 0.5f;
        [SerializeField] private AudioClip[] _clipVariants;
        private AudioSource _audioSource;

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("GravityCubeBlocker"))
            {
                // Detach if cube is touching specific wall.
                RequestDetach?.Invoke();
            }

            if (other.gameObject.TryGetComponent(out PlayerMovement player))
            {
                // Detach if player is on the cube.

            }

            if (other.impulse.magnitude > minImpact)
            {
                // Play impact sound on collision.
                AudioClip clip = _clipVariants[Random.Range(0, _clipVariants.Length)];

                if (!_audioSource)
                    _audioSource = GetComponent<AudioSource>();

                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.PlayOneShot(clip);
            }
        }

        private IEnumerator DetachFix()
        {
            RequestDetach?.Invoke();
            var col = GetComponent<BoxCollider>();
            col.enabled = false;
            yield return new WaitForSeconds(0.1f);
            col.enabled = true;
        }
    }
}
