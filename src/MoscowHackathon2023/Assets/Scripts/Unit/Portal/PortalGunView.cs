using Unit.Weapon;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.Portal
{
    public class PortalGunView : MonoBehaviour, IWeaponedView
    {
        public UnityEvent PickedUp { get; } = new UnityEvent();
        public UnityEvent Released { get; } = new UnityEvent();

        public void PickUp(Transform placeInHand) 
        {
            PickedUp?.Invoke();
            
            gameObject.transform.parent = placeInHand;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
        }
        public void Release() 
        {            
            gameObject.transform.parent = null;
            Released?.Invoke();
        }
    }
}

