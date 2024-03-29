﻿using System.Threading.Tasks;
using Data.StaticData.GunData;
using Data.StaticData.GunData.ScaleGunData;
using Services.Containers;
using Services.Input;
using Services.PlaySounds;
using Unit.UniversalGun;
using Unit.Weapon;
using UnityEngine;

namespace Unit.ScaleGun
{
    public class ScaleGun : IWeaponed
    {
        public ScaleGun(PlayerInputActionReader playerInputActionReader,
            ScaleGunData scaleGunData,
            IGameInstancesContainer gameInstancesContainer,
            IPlaySoundsService playSoundsService)
        {
            _playerInputActionReader = playerInputActionReader;

            _gameInstancesContainer = gameInstancesContainer;
            _playSoundsService = playSoundsService;

            _scaleGunData = scaleGunData;
        }

        public void SetUpUniversalView(UniversalGunView universalGunView)
        {
            _universalGunView = universalGunView;
            
            _universalGunView.ScaleGunBody.SetActive(true);
        }

        public BaseGunData GunData => _scaleGunData;

        private PlayerInputActionReader _playerInputActionReader;

        private IScalable _currentScalableObject;

        private readonly ScaleGunData _scaleGunData;

        private readonly IGameInstancesContainer _gameInstancesContainer;
        private readonly IPlaySoundsService _playSoundsService;
        private UniversalGunView _universalGunView;

        private bool _isLeftMouseHeld;
        private bool _isRightMouseHeld;

        private Transform _placeWeaponInHand;
        
        private float _raiseSoundTimer;
        private float _fallSoundTimer;

        private void TryFindScalableObject()
        {
            var ray = _gameInstancesContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            int layerToIgnore = LayerMask.NameToLayer("IgnoreWeaponRay");
            int layerMask = ~(1 << layerToIgnore);
            if (!Physics.Raycast(ray, out var hit, 10, layerMask))
            {
                return;
            }

            if (!hit.collider.TryGetComponent(out IScalable scalable))
            {
                return;
            }

            _currentScalableObject = scalable;
        }

        public void Select() 
        {
            if (_universalGunView == null)
            {
                return;
            }

            _playerInputActionReader.IsLeftButtonClickStarted += StartLeftMouseHeld;
            _playerInputActionReader.IsLeftButtonClickEnded += EndLeftMouseHeld;

            _playerInputActionReader.IsRightButtonClickStarted += StartRightMouseHeld;
            _playerInputActionReader.IsRightButtonClickEnded += EndRightMouseHeld;
            
            _universalGunView.ChangeActiveGun(GunTypes.Scale);
        }

        public void Deselect() 
        {
            _playerInputActionReader.IsLeftButtonClickStarted -= StartLeftMouseHeld;
            _playerInputActionReader.IsLeftButtonClickEnded -= EndLeftMouseHeld;

            _playerInputActionReader.IsRightButtonClickStarted -= StartRightMouseHeld;
            _playerInputActionReader.IsRightButtonClickEnded -= EndRightMouseHeld;
        }

        private void StartLeftMouseHeld()
        {
            _isLeftMouseHeld = true;
            MainFire();
        }

        private void EndLeftMouseHeld()
        {
            _isLeftMouseHeld = false;
            _currentScalableObject = null;
        }
        
        private void StartRightMouseHeld()
        {
            _isRightMouseHeld = true;
            AlternateFire();
        }

        private void EndRightMouseHeld()
        {
            _isRightMouseHeld = false;
            _currentScalableObject = null;
        }

        public async void MainFire()
        {
            TryFindScalableObject();

            while (_isLeftMouseHeld)
            {
                _currentScalableObject?.UpScale(_scaleGunData.ResizeValue);

                await Task.Yield();
                
                _raiseSoundTimer += Time.deltaTime;

                if (_raiseSoundTimer >= _scaleGunData.SoundPlayDelay)
                {
                    _playSoundsService.PlayAudioClip(GunData.FirstGunSound, 0.1f, true, false);

                    _raiseSoundTimer = 0;
                }
            }
        }

        public async void AlternateFire()
        {
            TryFindScalableObject();

            while (_isRightMouseHeld)
            {
                _currentScalableObject?.DownScale(_scaleGunData.ResizeValue);

                await Task.Yield();
                
                _raiseSoundTimer += Time.deltaTime;

                if (_raiseSoundTimer >= _scaleGunData.SoundPlayDelay)
                {
                    _playSoundsService.PlayAudioClip(GunData.SecondGunSound, 0.1f, true, false);

                    _raiseSoundTimer = 0;
                }
            }
        }
    }
}