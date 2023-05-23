using System.Collections;
using System.Timers;
using Data.StaticData.GunData;
using Data.StaticData.GunData.MountRemoveData;
using Infrastructure;
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
        public void SetUpUniversalView(UniversalGunView universalGunView)
        {
            throw new System.NotImplementedException();
        }

        public BaseGunData GunData  => _mountRemoveData;

        public MountRemote(PlayerInputActionReader playerInputActionReader,
            IGameInstancesContainer gameInstancesContainer,
            MountRemoveData mountRemoveData,
            UniversalGunView universalGunView,
            MountView mountView, 
            Transform positionInHand,
            ICoroutineRunner coroutineRunner)
        {
            _playerInputActionReader = playerInputActionReader;
            
            _gameInstancesContainer = gameInstancesContainer;
            _mountRemoveData = mountRemoveData;
            _universalGunView = universalGunView;
            _mountView = mountView;
            _positionInHand = positionInHand;
            _coroutineRunner = coroutineRunner;
        }
        
        private readonly MountRemoveData _mountRemoveData;
        
        private readonly PlayerInputActionReader _playerInputActionReader;
        
        private readonly IGameInstancesContainer _gameInstancesContainer;

        private readonly UniversalGunView _universalGunView;

        private readonly MountView _mountView;
        private readonly Transform _positionInHand;
        private readonly ICoroutineRunner _coroutineRunner;

        private GameObject _currentTarget;

        private bool _isMountCameraActive;

        private bool _isMountInGun = true;

        public void Select()
        {
            _playerInputActionReader.IsLeftButtonClicked += MainFire;
            _playerInputActionReader.IsRightButtonClicked += AlternateFire;

            _playerInputActionReader.IsPlayerTabClicked += OnTabClicked;
            
            _universalGunView.ChangeActiveGun(GunTypes.Mount);

            _currentTarget = Object.Instantiate(_mountView.TargetPrefab, _mountView.transform.position, Quaternion.identity);

            SetMountToGun();

            _mountView.SetTarget(_currentTarget.transform);
            
            _mountView.gameObject.SetActive(false);
        }

        public void Deselect()
        {
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsRightButtonClicked -= AlternateFire;
            
            _playerInputActionReader.IsPlayerTabClicked -= OnTabClicked;
            
            AlternateFire();
            
            Object.Destroy(_currentTarget);
            
            _isMountInGun = true;
            
            _mountView.SetTarget(null);
        }

        private void SetMountToGun()
        {
            var transform = _mountView.transform;

            transform.position = _universalGunView.GuineaPigAttachPoint.position;

            _mountView.transform.SetParent(_positionInHand);
        }

        private void OnTabClicked()
        {
            if (_mountView.gameObject.activeSelf == false)
            {
                return;
            }
            
            if (_isMountCameraActive)
            {
                AlternateFire();

                _isMountCameraActive = false;
                
                return;
            }
            
            Cursor.lockState = CursorLockMode.None;
            _mountView.MountCamera.Priority = _gameInstancesContainer.CinemachineBrain.ActiveVirtualCamera.Priority + 1;
            
            _isMountCameraActive = true;
        }

        public void MainFire()
        {
            if (_isMountInGun)
            {
                _mountView.transform.SetParent(null);
                _mountView.gameObject.SetActive(true);

                _mountView.transform.rotation = new Quaternion(0, 0, 0, 0);
            
                _mountView.MountRigidbody.AddForce(_universalGunView.GuineaPigAttachPoint.forward * _mountRemoveData.DropForce, ForceMode.Impulse);

                _isMountInGun = false;

                _coroutineRunner.StartCoroutine(WaitForMountFall());

                return;
            }

            var ray = _gameInstancesContainer.Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            _mountView.Target.position = hit.point;
        }

        public void AlternateFire()
        {
            _isMountInGun = true;
            
            Cursor.lockState = CursorLockMode.Locked;
            _mountView.MountCamera.Priority = 0;
        }

        private IEnumerator WaitForMountFall()
        {
            yield return new WaitForSeconds(1f);

            _mountView.AiPath.enabled = true;
        }
    }
}