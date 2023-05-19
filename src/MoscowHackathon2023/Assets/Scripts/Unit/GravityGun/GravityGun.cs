using System.Collections;
using Data.StaticData.GunData;
using Data.StaticData.GunData.GravityGunData;
using Services.Containers;
using Services.Input;
using Unit.Weapon;
using UnityEngine;

namespace Unit.GravityGun
{
    public class GravityGun : IWeaponed
    {
        public BaseGunData GunData => _gravityGunGravityGunData;
        
        private Rigidbody _currentRigidbody;
        private GravityGunView _gravityGunView;
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _dragIn;
        private PlayerInputActionReader _playerInputActionReader;
        private readonly GravityGunData _gravityGunGravityGunData;
        private readonly ICameraContainer _cameraContainer;
        private readonly Transform _placeWeaponInHand;
        private LayerMask _interactiveLayer = LayerMask.NameToLayer("InteractiveObjectForGravity");
        private LayerMask _grabbedLayer = LayerMask.NameToLayer("Grabbed");


        public GravityGun(ICoroutineRunner coroutineRunner,
            GravityGunView gravityGunView, 
            PlayerInputActionReader playerInputActionReader,
            GravityGunData gravityGunGravityGunData,
            ICameraContainer cameraContainer,
            Transform placeWeaponInHand)
        {
            _gravityGunView = gravityGunView;
            _coroutineRunner = coroutineRunner;
            _playerInputActionReader = playerInputActionReader;
            _gravityGunGravityGunData = gravityGunGravityGunData;
            _cameraContainer = cameraContainer;
            _placeWeaponInHand = placeWeaponInHand;
            _gravityGunView.PickedUp.AddListener(PickUp);
            _gravityGunView.Released.AddListener(Release);
        }

        public void MainFire()
        {
            Debug.Log(_currentRigidbody);
            
            var ray = _cameraContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (!Physics.Raycast(ray, out var hit, _gravityGunGravityGunData.CatchDistance))
            {
                return;
            }

            if (hit.collider.gameObject.layer != _interactiveLayer)
            {
                return;
            }
            
            
            _currentRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
            _dragIn = _coroutineRunner.StartCoroutine(DragIn());
            
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsLeftButtonClicked += StopMainFire;

                    
            if (_currentRigidbody == null)
            {
                Debug.LogError("InteractiveObjectForGravity does not have a Rigidbody");
            }

            _currentRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _currentRigidbody.gameObject.layer = _grabbedLayer;
        }

        private void StopMainFire()
        {
            _coroutineRunner.StopCoroutine(_dragIn);
            _playerInputActionReader.IsLeftButtonClicked -= StopMainFire;
            _playerInputActionReader.IsLeftButtonClicked += MainFire;

            if (_currentRigidbody != null)
            {
                _currentRigidbody.constraints = RigidbodyConstraints.None;
                _currentRigidbody.gameObject.layer = _interactiveLayer;
                _currentRigidbody = null;
            }
        }

        public void AlternateFire()
        {
            if (_currentRigidbody == null)
            {
                return;
            }
            
            _coroutineRunner.StopCoroutine(_dragIn);
            _currentRigidbody.velocity = _gravityGunView.PointGravity.forward * _gravityGunGravityGunData.DropPower;
                
            _currentRigidbody.constraints = RigidbodyConstraints.None;
            _currentRigidbody.gameObject.layer = _interactiveLayer;

            _currentRigidbody = null;
        }

        public void PickUp() 
        {         
            _playerInputActionReader.IsLeftButtonClicked += MainFire;
            _playerInputActionReader.IsRightButtonClicked += AlternateFire;

            _gravityGunView.ShowInHand(_placeWeaponInHand);
        }

        public void Release() 
        {
            if (_dragIn != null) 
            { 
                _coroutineRunner.StopCoroutine(_dragIn);            
            }

            _playerInputActionReader.IsLeftButtonClicked -= StopMainFire;
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsRightButtonClicked -= AlternateFire;

            _gravityGunView.HideInHand();          
        }
        
        private IEnumerator DragIn()
        {
            while (true) 
            { 
                _currentRigidbody.velocity =
                    (_gravityGunView.PointGravity.position - 
                     (_currentRigidbody.transform.position + _currentRigidbody.centerOfMass)) * _gravityGunGravityGunData.CatchPower;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
