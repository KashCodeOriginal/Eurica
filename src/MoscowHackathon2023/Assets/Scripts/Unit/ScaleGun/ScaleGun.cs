using System.Threading.Tasks;
using Data.StaticData.GunData.ScaleGunData;
using Services.Containers;
using Services.Input;
using Unit.Weapon;
using UnityEngine;

namespace Unit.ScaleGun
{
    public class ScaleGun : IWeaponed
    {
        public ScaleGun(PlayerInputActionReader playerInputActionReader, 
            ScaleGunView scaleGunView,
            ScaleGunData scaleGunData,
            ICameraContainer cameraContainer)
        {
            _playerInputActionReader = playerInputActionReader;

            _cameraContainer = cameraContainer;

            _scaleGunView = scaleGunView;
            _scaleGunData = scaleGunData;

            _scaleGunView.PickedUp.AddListener(PickUp);
            _scaleGunView.Released.AddListener(Release);
        }

        private PlayerInputActionReader _playerInputActionReader;

        private IScalable _currentScalableObject;

        private ScaleGunView _scaleGunView;
        private readonly ScaleGunData _scaleGunData;

        private readonly ICameraContainer _cameraContainer;

        private RaycastHit[] _hit = new RaycastHit[2];
        
        private LayerMask _layerMask = LayerMask.NameToLayer("Walls");

        private bool _isLeftMouseHeld;
        private bool _isRightMouseHeld;
        
        private void TryFindScalableObject()
        {
            var ray = _cameraContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            if (!hit.collider.TryGetComponent(out IScalable scalable))
            {
                return;
            }

            _currentScalableObject = scalable;
        }

        private void PickUp() 
        {
            _playerInputActionReader.IsLeftButtonClickStarted += StartLeftMouseHeld;
            _playerInputActionReader.IsLeftButtonClickEnded += EndLeftMouseHeld;
            
            _playerInputActionReader.IsRightButtonClickStarted += StartRightMouseHeld;
            _playerInputActionReader.IsRightButtonClickEnded += EndRightMouseHeld;
        }

        private void Release() 
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
            }
        }

        public async void AlternateFire()
        {
            TryFindScalableObject();

            while (_isRightMouseHeld)
            {
                _currentScalableObject?.DownScale(_scaleGunData.ResizeValue);

                await Task.Yield();
            }
        }
    }
}