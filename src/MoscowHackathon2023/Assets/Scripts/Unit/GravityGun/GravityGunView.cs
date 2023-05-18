using Unit.Weapon;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.GravityGun
{
    public class GravityGunView : MonoBehaviour, IWeaponedView
    {        
        [SerializeField] private Transform _pointGravity;
        public Transform PointGravity => _pointGravity;

        public UnityEvent PickedUp { get; } = new UnityEvent();
        public UnityEvent Released { get; } = new UnityEvent();

        public void PickUp(Transform placeInHand)
        {
            PickedUp?.Invoke();
            gameObject.transform.parent = placeInHand;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
        }

        public void Release() {
            Released?.Invoke();
            gameObject.transform.parent = null;            
        }
    }
}
