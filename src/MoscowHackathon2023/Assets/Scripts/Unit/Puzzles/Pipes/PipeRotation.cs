using Unit.Base;
using UnityEngine;

namespace Unit.Puzzles.Pipes
{
    public class PipeRotation : MonoBehaviour, IInteractable
    {
        [SerializeField] private RotationAxis _rotationAxis;
        
        public void Interact()
        {
            var currentRotationAxis = _rotationAxis switch
            {
                RotationAxis.Vertical => new Vector3(0, 0, 0.5f),
                RotationAxis.Horizontal => new Vector3(0, 0.5f, 0),
                _ => new Vector3()
            };
            
            transform.Rotate(currentRotationAxis, Space.World);
        }
    }
}