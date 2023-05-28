using System.Collections;
using Unit.Base;
using Unit.TriggerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.Lever
{
    [RequireComponent(typeof(TriggerTaskHelper))]
    [RequireComponent(typeof(SoundHelper))]
    [RequireComponent(typeof(BoxCollider))]
    public class LeverButtonLogic : MonoBehaviour, IInteractable
    {
        public UnityEvent OnPress;
        public UnityAction<bool> OnStateChanged;
        private bool _canPress = true;
        private bool _pressing = false;
        [SerializeField] private float _pressTime = 0.2f;
        [SerializeField] private float _releaseTime = 1f;
        [SerializeField] private Color _gizmoColor;

        public void TryPressing()
        {
            if (_canPress)
            {
                // Lever acts like a button, press and return to position.
                StartCoroutine(PressAndReturn());
            }
            else
            {
                // Wait till animation ends.
            }
        }

        public void Interact()
        {
            TryPressing();
        }

        private IEnumerator PressAndReturn()
        {
            _canPress = false;
            _pressing = true;
            OnStateChanged?.Invoke(true);
            GetComponent<SoundHelper>().SetPitch(0.95f, 1.05f);
            GetComponent<SoundHelper>().PlaySound();

            yield return new WaitForSeconds(_pressTime);

            OnPress?.Invoke();
            _pressing = false;
            OnStateChanged?.Invoke(false);

            yield return new WaitForSeconds(_releaseTime);

            _canPress = true;
        }

        public float GetSpeed()
        {
            if (_pressing)
                return 1f / Mathf.Sqrt(_pressTime) * 10;
            else
                return 1f / Mathf.Sqrt(_releaseTime) * 10;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            var collider = GetComponent<BoxCollider>();
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(collider.center, collider.size);
        }
    }
}
