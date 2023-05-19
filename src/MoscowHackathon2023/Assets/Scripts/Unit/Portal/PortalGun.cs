using Services.Input;
using System.Timers;
using Data.StaticData.GunData;
using Data.StaticData.GunData.PortalGunData;
using Services.Containers;
using Services.Factories.PortalFactory;
using Unit.UniversalGun;
using Unit.Weapon;
using UnityEngine;

namespace Unit.Portal 
{ 
    public class PortalGun : IWeaponed
    {
        public BaseGunData GunData => _portalGunData;
        
        private IPortalFactory _portalFactory;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly ICameraContainer _cameraContainer;
        private readonly PortalGunData _portalGunData;
        private readonly UniversalGunView _universalGunView;
        
        private LayerMask _layerMask = LayerMask.NameToLayer("WallForPortal");


        public PortalGun(IPortalFactory portalFactory,
            PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer,
            PortalGunData portalGunData,
            UniversalGunView universalGunView)
        {
            _portalFactory = portalFactory;
            _playerInputActionReader = playerInputActionReader;
            _cameraContainer = cameraContainer;
            _portalGunData = portalGunData;
            _universalGunView = universalGunView;
        }

        public void MainFire() 
            => Fire(PortalType.Blue);

        public void AlternateFire() 
            => Fire(PortalType.Red);

        public void Select() 
        {         
            _playerInputActionReader.IsRightButtonClicked += MainFire;
            _playerInputActionReader.IsLeftButtonClicked += AlternateFire;
            
            _universalGunView.ChangeActiveGun(GunTypes.Portal);
        }

        public void Deselect() 
        { 
            _playerInputActionReader.IsRightButtonClicked -= MainFire;
            _playerInputActionReader.IsLeftButtonClicked -= AlternateFire;
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

