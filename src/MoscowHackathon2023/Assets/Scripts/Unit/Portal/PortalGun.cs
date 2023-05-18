﻿using Services.Input;
using System.Timers;
using Services.Containers;
using Unit.Weapon;
using UnityEngine;

namespace Unit.Portal 
{ 
    public class PortalGun : IWeaponed
    {
        private PortalFactory _portalFactory;
        private readonly PortalGunView _portalGunView;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly ICameraContainer _cameraContainer;
        private LayerMask _layerMask = LayerMask.NameToLayer("Walls");

        public PortalGun(PortalFactory portalFactory, 
            PortalGunView portalGunView, 
            PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer)
        {
            _portalFactory = portalFactory;
            _portalGunView = portalGunView;
            _playerInputActionReader = playerInputActionReader;
            _cameraContainer = cameraContainer;
            _portalGunView.PickedUp.AddListener(PickUp);
            _portalGunView.Released.AddListener(Release);
        }

        public void MainFire() 
            => Fire(PortalType.Blue);
        public void AlternateFire() 
            => Fire(PortalType.Red);

        private void PickUp() 
        {         
            _playerInputActionReader.IsRightButtonClicked += MainFire;
            _playerInputActionReader.IsLeftButtonClicked += AlternateFire;
        }

        private void Release() 
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

