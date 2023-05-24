using System.Threading.Tasks;
using Data.StaticData.GunData;
using Data.StaticData.GunData.ScaleGunData;
using Services.Containers;
using Services.Input;
using Unit.UniversalGun;
using Unit.Weapon;
using UnityEngine;

namespace Unit.ScaleGun
{
    public class ScaleGun : IWeaponed
    {
        public ScaleGun(PlayerInputActionReader playerInputActionReader,
            ScaleGunData scaleGunData,
            IGameInstancesContainer gameInstancesContainer)
        {
            _playerInputActionReader = playerInputActionReader;

            _gameInstancesContainer = gameInstancesContainer;

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
        private UniversalGunView _universalGunView;

        private bool _isLeftMouseHeld;
        private bool _isRightMouseHeld;

        private Transform _placeWeaponInHand;

        private LayerMask _ignoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        

        private void TryFindScalableObject()
        {
            var ray = _gameInstancesContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (!Physics.Raycast(ray, out var hit, 1000f, ~_ignoreRaycast))
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