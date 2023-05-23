using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneControllers.Scene4
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
