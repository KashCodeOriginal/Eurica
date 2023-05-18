using Data.StaticData.GunData;
using Data.StaticData.GunData.GravityGunData;
using Data.StaticData.GunData.MountRemoveData;
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
        public BaseGunData GunData  => _mountRemoveData;

        public MountRemote(PlayerInputActionReader playerInputActionReader,
            MountRemoteView mountRemoteView, 
            MountView mountView,
            ICameraContainer cameraContainer,
            MountRemoveData mountRemoveData,
            Transform placeWeaponInHand)
        {
            _playerInputActionReader = playerInputActionReader;
            _mountRemoteView = mountRemoteView;

            _mountView = mountView;
            _cameraContainer = cameraContainer;
            _mountRemoveData = mountRemoveData;
            _placeWeaponInHand = placeWeaponInHand;

            _mountRemoteView.PickedUp.AddListener(PickUp);
            _mountRemoteView.Released.AddListener(Release);
        }

        private readonly PlayerInputActionReader _playerInputActionReader;
        
        private readonly MountRemoteView _mountRemoteView;

        private GameObject _currentTarget;

        private readonly MountView _mountView;

        private readonly ICameraContainer _cameraContainer;
        private readonly BaseGunData _mountRemoveData;
        private readonly Transform _placeWeaponInHand;


        public void PickUp()
        {
            _playerInputActionReader.IsLeftButtonClicked += MainFire;
            _playerInputActionReader.IsRightButtonClicked += AlternateFire;

            _currentTarget = Object.Instantiate(_mountView.TargetPrefab, _mountView.transform.position, Quaternion.identity);
            
            _mountView.SetTarget(_currentTarget.transform);
            
            _mountRemoteView.ShowInHand(_placeWeaponInHand);
        }

        public void Release()
        {
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsRightButtonClicked -= AlternateFire;
            
            AlternateFire();
            
            Object.Destroy(_currentTarget);
            
            _mountView.SetTarget(null);
            
            _mountRemoteView.HideInHand();
        }

        public void MainFire()
        {
            Cursor.lockState = CursorLockMode.None;
            
            _mountView.MountCamera.Priority = _cameraContainer.CinemachineBrain.ActiveVirtualCamera.Priority + 1;

            var ray = _cameraContainer.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            _mountView.Target.position = hit.point;
        }

        public void AlternateFire()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            _mountView.MountCamera.Priority = 0;
        }
    }
}