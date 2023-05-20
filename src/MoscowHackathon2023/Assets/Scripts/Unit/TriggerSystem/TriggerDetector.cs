using UnityEngine;
using UnityEngine.Events;

namespace Unit.TriggerSystem
{
    [RequireComponent(typeof(TriggerTaskHelper))]
    [RequireComponent(typeof(BoxCollider))]
    public class TriggerDetector : MonoBehaviour
    {
        [SerializeField] private Color _gizmoColor = Color.green;
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        
        private bool _isTriggered;

        public void Enter()
        {
            if (_isTriggered)
            {
                return;
            }

            OnEnter?.Invoke();

            _isTriggered = true;
        }

        public void Exit()
        {
            OnExit?.Invoke();

            _isTriggered = false;
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
