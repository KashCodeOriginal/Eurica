using UnityEngine;
using UnityEngine.Events;

namespace Unit.TriggerSystem
{
    [RequireComponent(typeof(TriggerTaskHelper))]
    [RequireComponent(typeof(BoxCollider))]
    public class TriggerDetector : MonoBehaviour
    {
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
            Gizmos.color = Color.green;
            var collider = GetComponent<BoxCollider>();
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(collider.center, collider.size);
        }
    }
}
