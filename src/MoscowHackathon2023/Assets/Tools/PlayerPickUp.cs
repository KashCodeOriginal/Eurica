using Services.Containers;
using Unit.Weapon;
using UnityEngine;
using Zenject;

namespace Tools
{
    public class PlayerPickUp : MonoBehaviour
    {
        [SerializeField] private Transform _placeInHand;
        
        private IWeaponedView _currentWeaponView;
        private LayerMask _layerMask;

        private ICameraContainer _cameraContainer;
        
        public void Construct(ICameraContainer cameraContainer)
        {
            _cameraContainer = cameraContainer;
        }
        
        private void Start()
        {
            _layerMask = LayerMask.NameToLayer("PickUp");
        }
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E))
            {
                return;
            }

            if (_cameraContainer == null)
            {
                return;
            }
        
            var ray = _cameraContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f)); //Center the screen in the crosshairs

            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            if (hit.collider.gameObject.layer != _layerMask)
            {
                return;
            }
            
            IWeaponedView weaponedView = hit.collider.gameObject.GetComponent<IWeaponedView>();

            if (weaponedView == null)
            {
                return;
            }

            _currentWeaponView?.Release();
            
            weaponedView.PickUp(_placeInHand);
            
            _currentWeaponView = weaponedView;
        }
    }
}
