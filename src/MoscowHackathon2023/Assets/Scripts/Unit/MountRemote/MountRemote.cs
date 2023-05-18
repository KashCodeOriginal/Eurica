using Data.AssetsAddressablesConstants;
using Services.AssetsAddressables;
using Services.Containers;
using Services.Input;
using Unit.Mount;
using Unit.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unit.MountRemote
{
    public class MountRemote : IWeaponed
    {
        public MountRemote(PlayerInputActionReader playerInputActionReader,
            MountRemoteView mountRemoteView, 
            MountView mountView,
            ICameraContainer cameraContainer)
        {
            _playerInputActionReader = playerInputActionReader;
            
            _mountRemoteView = mountRemoteView;
            
            _mountView = mountView;
            _cameraContainer = cameraContainer;

            _mountRemoteView.PickedUp.AddListener(Pickup);
            _mountRemoteView.Released.AddListener(Release);
        }

        private readonly PlayerInputActionReader _playerInputActionReader;

        private readonly MountRemoteView _mountRemoteView;

        private GameObject _currentTarget;

        private readonly MountView _mountView;

        private readonly ICameraContainer _cameraContainer;

        private void Pickup()
        {
            _playerInputActionReader.IsLeftButtonClicked += MainFire;
            _playerInputActionReader.IsRightButtonClicked += AlternateFire;

            _currentTarget = Object.Instantiate(_mountView.TargetPrefab);
            
            _mountView.SetTarget(_currentTarget.transform);
        }

        private void Release()
        {
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsRightButtonClicked -= AlternateFire;
            
            _currentTarget = null;
            
            _mountView.SetTarget(null);
        }

        public void MainFire()
        {
            _mountView.MountCamera.enabled = true;

            var ray = _cameraContainer.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            _mountView.Target.position = hit.point;
        }

        public void AlternateFire()
        {
            _mountView.MountCamera.enabled = false;
        }

        private async void SetUp()
        {
           
        }
    }
}