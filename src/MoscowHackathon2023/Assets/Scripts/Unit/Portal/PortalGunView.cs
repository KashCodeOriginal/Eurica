using Unit.Portal;
using UnityEngine;
using UnityEngine.Events;

namespace PortalMechanics
{
    public class PortalGunView : MonoBehaviour, IWeaponedView
    {
        private UnityEvent _pickedUp = new UnityEvent();
        private UnityEvent _released = new UnityEvent();

        public UnityEvent PickedUp { get => _pickedUp; }
        public UnityEvent Released { get => _released; }

        public void PickUp(Transform placeInHand) {
            _pickedUp?.Invoke();
            this.gameObject.transform.parent = placeInHand;
            this.gameObject.transform.localPosition = Vector3.zero;
            this.gameObject.transform.localRotation = Quaternion.identity;
        }
        public void Release() {            
            this.gameObject.transform.parent = null;
            _released?.Invoke();
        }
    }
}

