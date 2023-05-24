using UnityEngine;

namespace Unit.SceneControllers.Scene4
{
    public class Scene4Ladder : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        // Controlled by PlayerButton.
        public void SetLadderState(bool state)
        {
            _animator.SetBool("Built", state);
        }
    }
}
