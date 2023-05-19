using UnityEngine;
using UnityEngine.Events;

namespace TriggerSystem
{
    [RequireComponent(typeof(TriggerTaskHelper))]
    public class TriggerDetector : MonoBehaviour
    {
        public UnityEvent OnEnter;
        public UnityEvent OnExit;

        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}
