using UnityEngine;
using UnityEngine.Events;

namespace Unit.TriggerSystem
{
    [RequireComponent(typeof(TriggerTaskHelper))]
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
    }
}
