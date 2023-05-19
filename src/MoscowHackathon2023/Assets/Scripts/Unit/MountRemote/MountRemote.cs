using Data.StaticData.GunData;
using Data.StaticData.GunData.MountRemoveData;
using Services.Containers;
using Services.Input;
using Unit.Mount;
using Unit.UniversalGun;
using Unit.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unit.MountRemote
{
    public class MountRemote : IWeaponed
    {
        public BaseGunData GunData  => _mountRemoveData;

        public MountRemote(PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer,
            MountRemoveData mountRemoveData, 
            UniversalGunView universalGunView,
            MountView mountView)
        {
            _playerInputActionReader = playerInputActionReader;
            
            _cameraContainer = cameraContainer;
            _mountRemoveData = mountRemoveData;
            _universalGunView = universalGunView;
            _mountView = mountView;
        }

        private readonly PlayerInputActionReader _playerInputActionReader;

        private GameObject _currentTarget;

        private readonly ICameraContainer _cameraContainer;
        private readonly BaseGunData _mountRemoveData;
        private readonly UniversalGunView _universalGunView;
        private readonly MountView _mountView;

        public void Select()
        {
            _playerInputActionReader.IsLeftButtonClicked += MainFire;
            _playerInputActionReader.IsRightButtonClicked += AlternateFire;
            
            _universalGunView.ChangeActiveGun(GunTypes.Mount);

            _currentTarget = Object.Instantiate(_mountView.TargetPrefab, _mountView.transform.position, Quaternion.identity);
            
            _mountView.SetTarget(_currentTarget.transform);
            
            _currentTarget.SetActive(false);
        }

        public void Deselect()
        {
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsRightButtonClicked -= AlternateFire;
            
            AlternateFire();
            
            Object.Destroy(_currentTarget);
            
            _mountView.SetTarget(null);
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