using Services.Input;
using System;
using System.Collections;
using Unit.Portal;
using Unit.Weapon;
using UnityEngine;

namespace Unit.GravityGunMechanics
{
    public class GravityGun : IWeaponed 
    { 
        private float _catchDistance = 5;
        private float _catchPower = 10;
        private float _dropPower = 20;
        private Rigidbody _currentRigidbody;
        private GravityGunView _gravityGunView;
        private ICoroutineRunner _coroutinerRunner;
        private Coroutine _dragIn;
        private PlayerInputActionReader _playerInputActionReader;
        private LayerMask layerMask = LayerMask.NameToLayer("InteractiveObjectForGravity");

        public GravityGun(ICoroutineRunner coroutinerRunner, GravityGunView gravityGunView, PlayerInputActionReader playerInputActionReader)
        {
            _gravityGunView = gravityGunView;
            _coroutinerRunner = coroutinerRunner;
            _playerInputActionReader = playerInputActionReader;
            _gravityGunView.PickedUp.AddListener(PickUp);
            _gravityGunView.Released.AddListener(Release);
        }

        public void MainFire()
        {
            Debug.Log(_currentRigidbody);
            
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)); //Center the screen in the crosshairs

            if (Physics.Raycast(ray, out var hit, _catchDistance))
            {
                if (hit.collider.gameObject.layer == layerMask)
                {
                    _currentRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
                    _dragIn = _coroutinerRunner.StartCoroutine(DragIn());
                    _playerInputActionReader.IsLeftButtonClicked -= MainFire;
                    _playerInputActionReader.IsLeftButtonClicked += StopMainFire;
                    if (_currentRigidbody == null)
                    {
                        Debug.LogError("InteractiveObjectForGravity does not have a Rigidbody");
                    } 
                }
            }         
        }

        private void StopMainFire()
        {
            _coroutinerRunner.StopCoroutine(_dragIn);
            _playerInputActionReader.IsLeftButtonClicked -= StopMainFire;
            _playerInputActionReader.IsLeftButtonClicked += MainFire;
        }

        //On a different "Fire" button
        public void AlternateFire()
        {
            if (_currentRigidbody != null) 
            { 
                _coroutinerRunner.StopCoroutine(_dragIn);
                _currentRigidbody.velocity = _gravityGunView.PointGravity.forward * _dropPower;
                _currentRigidbody = null;
            }            
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
                _currentRigidbody.velocity = (_gravityGunView.PointGravity.position - (_currentRigidbody.transform.position + _currentRigidbody.centerOfMass)) * _catchPower;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
