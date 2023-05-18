using UnityEngine;

namespace PortalMechanics
{
    public class Teleporter : MonoBehaviour
    {
        private Teleporter _other;
        private Vector3 _direction;
        private float _force;
        private Rigidbody _currentRigidbody;

        public void TurnOn(Teleporter other) {
            enabled = true;
            _other = other;
        }

        public void TurnOff() {
            enabled = false;
        }

        private void OnTriggerStay(Collider other)
        {
            float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;
            if (zPos < 0) Teleport(other.transform);
        }

        private void Teleport(Transform obj)
        {            
            Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
            localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
            obj.position = _other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

            Quaternion difference = _other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
            obj.rotation = difference * obj.rotation;

            if (_currentRigidbody != null) 
            {
                _direction = new Vector3(_direction.x *-1, _direction.y, _direction.z *-1);
                _currentRigidbody.AddForce(_direction * _force, ForceMode.Impulse);                
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            _currentRigidbody = other.GetComponent<Rigidbody>();            
            if (_currentRigidbody != null) 
            {
                Vector3 velocity = _currentRigidbody.velocity;       
                _direction = velocity.normalized;  
                _force = velocity.magnitude;                  
            }            
        }

        private void OnTriggerExit(Collider other)
        {
            _currentRigidbody = null;            
        }
    }
}
