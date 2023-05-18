using UnityEngine;

namespace Unit.Mount
{
    public class MountAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _moveHash = Animator.StringToHash("IsWalking");

        public void SetWalkingAnimation(bool condition)
        {
            _animator.SetBool(_moveHash, condition);
        }
    }
}