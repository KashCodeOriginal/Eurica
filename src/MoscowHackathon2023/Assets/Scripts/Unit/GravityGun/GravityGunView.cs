using Unit.Weapon;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.GravityGun
{
    public class GravityGunView : MonoBehaviour, IWeaponedView
    {        
        [SerializeField] private Transform _pointGravity;
        public Transform PointGravity => _pointGravity;

        public UnityEvent PickedUp { get; } = new();
        public UnityEvent Released { get; } = new();

        public void PickUp() 
        {
            PickedUp?.Invoke();
        }
        
        public void ShowInHand(Transform placeInHand) 
        {
            gameObject.transform.parent = placeInHand;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            
            gameObject.SetActive(true);
        }
        
        public void HideInHand()
        {
            gameObject.SetActive(false);
        }

        public void Release() 
        {            
            gameObject.transform.parent = null;
            Released?.Invoke();
        }
    }
}
