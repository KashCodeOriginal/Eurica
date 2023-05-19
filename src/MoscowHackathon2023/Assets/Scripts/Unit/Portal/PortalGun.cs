using Services.Input;
using System.Timers;
using Data.StaticData.GunData;
using Data.StaticData.GunData.PortalGunData;
using Services.Containers;
using Services.Factories.PortalFactory;
using Unit.Weapon;
using UnityEngine;

namespace Unit.Portal 
{ 
    public class PortalGun : IWeaponed
    {
        public BaseGunData GunData => _portalGunData;
        
        private IPortalFactory _portalFactory;
        private readonly PortalGunView _portalGunView;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly ICameraContainer _cameraContainer;
        private readonly PortalGunData _portalGunData;
        private readonly Transform _placeWeaponInHand;
        private LayerMask _layerMask = LayerMask.NameToLayer("Walls");


        public PortalGun(IPortalFactory portalFactory, 
            PortalGunView portalGunView, 
            PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer,
            PortalGunData portalGunData,
            Transform placeInHand)
        {
            _portalFactory = portalFactory;
            _portalGunView = portalGunView;
            _playerInputActionReader = playerInputActionReader;
            _cameraContainer = cameraContainer;
            _portalGunData = portalGunData;
            _placeWeaponInHand = placeInHand;
            _portalGunView.PickedUp.AddListener(PickUp);
            _portalGunView.Released.AddListener(Release);
        }

        public void MainFire() 
            => Fire(PortalType.Blue);

        public void AlternateFire() 
            => Fire(PortalType.Red);

        public void PickUp() 
        {         
            _playerInputActionReader.IsRightButtonClicked += MainFire;
            _playerInputActionReader.IsLeftButtonClicked += AlternateFire;
            
            _portalGunView.ShowInHand(_placeWeaponInHand);
        }

        public void Release() 
        { 
            _playerInputActionReader.IsRightButtonClicked -= MainFire;
            _playerInputActionReader.IsLeftButtonClicked -= AlternateFire;
            
            _portalGunView.HideInHand();
        }

        private void Fire(PortalType portalType) 
        { 
            var ray = _cameraContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));//Center the screen in the crosshairs

            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }
            
            if (hit.collider.gameObject.layer == _layerMask)
            {
                _portalFactory.CreatePortal(hit.point, hit.normal, portalType);                    
            }
        }
    }
}

