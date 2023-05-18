using System.Collections;
using Data.StaticData.GunData.GravityGunData;
using Services.Input;
using Unit.Weapon;
using UnityEngine;

namespace Unit.GravityGun
{
    public class GravityGun : IWeaponed 
    {
        private Rigidbody _currentRigidbody;
        private GravityGunView _gravityGunView;
        private ICoroutineRunner _coroutinerRunner;
        private Coroutine _dragIn;
        private PlayerInputActionReader _playerInputActionReader;
        private readonly GravityGunData _gravityGunData;
        private LayerMask _interactiveLayer = LayerMask.NameToLayer("InteractiveObjectForGravity");
        private LayerMask _grabbedLayer = LayerMask.NameToLayer("Grabbed");

        public GravityGun(ICoroutineRunner coroutinerRunner,
            GravityGunView gravityGunView, 
            PlayerInputActionReader playerInputActionReader,
            GravityGunData gravityGunData)
        {
            _gravityGunView = gravityGunView;
            _coroutinerRunner = coroutinerRunner;
            _playerInputActionReader = playerInputActionReader;
            _gravityGunData = gravityGunData;
            _gravityGunView.PickedUp.AddListener(PickUp);
            _gravityGunView.Released.AddListener(Release);
        }

        public void MainFire()
        {
            Debug.Log(_currentRigidbody);
            
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (!Physics.Raycast(ray, out var hit, _gravityGunData.CatchDistance))
            {
                return;
            }

            if (hit.collider.gameObject.layer != _interactiveLayer)
            {
                return;
            }
            
            
            _currentRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
            _dragIn = _coroutinerRunner.StartCoroutine(DragIn());
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsLeftButtonClicked += StopMainFire;

                    
            if (_currentRigidbody == null)
            {
                Debug.LogError("InteractiveObjectForGravity does not have a Rigidbody");
            }

            _currentRigidbody.gameObject.layer = _grabbedLayer;
        }

        private void StopMainFire()
        {
            _coroutinerRunner.StopCoroutine(_dragIn);
            _playerInputActionReader.IsLeftButtonClicked -= StopMainFire;
            _playerInputActionReader.IsLeftButtonClicked += MainFire;

            if (_currentRigidbody != null)
            {
                _currentRigidbody.gameObject.layer = _interactiveLayer;
            }
        }

        //On a different "Fire" button
        public void AlternateFire()
        {
            if (_currentRigidbody == null)
            {
                return;
            }
            
            _coroutinerRunner.StopCoroutine(_dragIn);
            _currentRigidbody.velocity = _gravityGunView.PointGravity.forward * _gravityGunData.DropPower;
                
            _currentRigidbody.gameObject.layer = _interactiveLayer;
                
            _currentRigidbody = null;
        }

        private void PickUp() 
        {         
            _playerInputActionReader.IsLeftButtonClicked += MainFire;
            _playerInputActionReader.IsRightButtonClicked += AlternateFire;
        }

        private void Release() 
        {
            if (_dragIn != null) 
            { 
                 _coroutinerRunner.StopCoroutine(_dragIn);            
            }
               
            _playerInputActionReader.IsLeftButtonClicked -= StopMainFire;
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsRightButtonClicked -= AlternateFire;            
        }
        
        private IEnumerator DragIn()
        {
            while (true) 
            { 
                _currentRigidbody.velocity =
                    (_gravityGunView.PointGravity.position - 
                     (_currentRigidbody.transform.position + _currentRigidbody.centerOfMass)) * _gravityGunData.CatchPower;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
