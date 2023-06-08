using Services.Input;
using System.Timers;
using Data.StaticData.GunData;
using Data.StaticData.GunData.PortalGunData;
using Services.Containers;
using Services.Factories.PortalFactory;
using Services.PlaySounds;
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
        private readonly IGameInstancesContainer _gameInstancesContainer;
        private readonly PortalGunData _portalGunData;
        private readonly IPlaySoundsService _playSoundsService;
        private UniversalGunView _universalGunView;
        
        private LayerMask _layerMask = LayerMask.NameToLayer("WallForPortal");

        public PortalGun(IPortalFactory portalFactory,
            PlayerInputActionReader playerInputActionReader,
            IGameInstancesContainer gameInstancesContainer,
            PortalGunData portalGunData,
            IPlaySoundsService playSoundsService)
        {
            _portalFactory = portalFactory;
            _playerInputActionReader = playerInputActionReader;
            _gameInstancesContainer = gameInstancesContainer;
            _portalGunData = portalGunData;
            _playSoundsService = playSoundsService;
        }

        public void SetUpUniversalView(UniversalGunView universalGunView)
        {
            _universalGunView = universalGunView;
            
            _universalGunView.PortalGunBody.SetActive(true);
        }

        public void MainFire()
        {
            Fire(PortalType.Blue);
            
            _playSoundsService.PlayAudioClip(GunData.FirstGunSound, _playSoundsService.GetVolumeLevel(VolumeLevel.Default), true, false);
        }

        public void AlternateFire()
        {
            Fire(PortalType.Red);
            
            _playSoundsService.PlayAudioClip(GunData.SecondGunSound, _playSoundsService.GetVolumeLevel(VolumeLevel.Default), true, false);
        }

        public void Select() 
        {         
            if (_universalGunView == null)
            {
                return;
            }

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
            var ray = _gameInstancesContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));//Center the screen in the crosshairs
            
            int layerToIgnore = LayerMask.NameToLayer("IgnoreWeaponRay");
            int layerMask = ~(1 << layerToIgnore);
            if (!Physics.Raycast(ray, out var hit, maxDistance: 10000, layerMask: layerMask))
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

