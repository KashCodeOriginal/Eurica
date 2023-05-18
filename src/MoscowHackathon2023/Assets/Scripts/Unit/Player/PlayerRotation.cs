using UnityEngine;

namespace Unit.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        public void Construct(Camera camera)
        {
            _camera = camera;
        }

        [SerializeField] private float _offset;

        private Camera _camera;

        private Quaternion _targetRotation;

        private void LateUpdate()
        {
            if (_camera == null)
            {
                return;
            }

            var cameraEuler = _camera.transform.rotation.eulerAngles;
            var transformEuler = transform.rotation.eulerAngles;
            
            _targetRotation =
                new Quaternion(cameraEuler.x - transformEuler.x, cameraEuler.y - transformEuler.y - _offset, cameraEuler.z - transformEuler.z, 0);

            transform.Rotate(new Vector3(0, _targetRotation.y, 0));
        }
        
        public void ForceTurnAngle(float value)
        {
            _targetRotation = new Quaternion(0, value, 0, 0);
        }
    }
}