using System.Collections;
using Unit.Base;
using Unit.TriggerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.Lever
{
    [RequireComponent(typeof(TriggerTaskHelper))]
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

            yield return new WaitForSeconds(_pressTime);

            Press();

            yield return new WaitForSeconds(_releaseTime);

            Release();

            _canPress = true;
        }

        private void Press()
        {
            _pressing = true;
            OnPress?.Invoke();
            OnStateChanged?.Invoke(true);
        }

        private void Release()
        {
            _pressing = false;
            OnStateChanged?.Invoke(false);
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
