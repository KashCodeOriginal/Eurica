using Unit.Portal;
using Unit.Weapon;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.GravityGunMechanics
{
    public class GravityGunView : MonoBehaviour, IWeaponedView
    {        
        [SerializeField] private Transform _pointGravity;
        public Transform PointGravity { get => _pointGravity; }

        private UnityEvent _pickedUp = new UnityEvent();
        private UnityEvent _released = new UnityEvent();

        public UnityEvent PickedUp { get => _pickedUp; }
        public UnityEvent Released { get => _released; }

        public void PickUp(Transform placeInHand)
        {
            _pickedUp?.Invoke();
            gameObject.transform.parent = placeInHand;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
        }

        public void Release() {
            _released?.Invoke();
            gameObject.transform.parent = null;            
        }
    }
}
