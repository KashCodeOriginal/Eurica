using UnityEngine;

namespace Unit.GravityGunMechanics
{
    public class GravityGun : MonoBehaviour
    {
        [SerializeField] private float _catchDistance;
        [SerializeField] private float _catchPower;
        [SerializeField] private float _dropPower;
        [SerializeField] private Transform _pointGravity;

        private Rigidbody _currentRigidbody;

        public void Fire() 
        {
            if (_currentRigidbody == null) 
            {
                var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)); //Center the screen in the crosshairs

                if (!Physics.Raycast(ray, out var hit, _catchDistance))
                {
                    return;
                }
                
                if (hit.collider.gameObject.layer != 11)
                {
                    return; //11 - InteractiveObjectForGravity
                }
                
                _currentRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
                
                if (_currentRigidbody == null)
                {
                    Debug.LogError("InteractiveObjectForGravity does not have a Rigidbody");
                }
            } 
            else 
            {
                DragIn();
            }            
        }

        //When the pressed button "Fire" is released
        public void Release() => _currentRigidbody = null;

        //On a different "Fire" button
        public void Drop() 
        {
            _currentRigidbody.velocity = _pointGravity.forward * _dropPower;
            Release();
        }

        private void DragIn() 
            => _currentRigidbody.velocity = (_pointGravity.position - (_currentRigidbody.transform.position + _currentRigidbody.centerOfMass)) * _catchPower;        
    }
}
