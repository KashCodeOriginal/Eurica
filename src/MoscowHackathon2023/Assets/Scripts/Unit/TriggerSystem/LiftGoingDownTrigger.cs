using UnityEngine;

namespace Unit.TriggerSystem
{
    public class LiftGoingDownTrigger : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int GoDown = Animator.StringToHash("GoDown");

        public void LiftGoDown()
        {
            _animator.SetTrigger(GoDown);
        }
    }
}
