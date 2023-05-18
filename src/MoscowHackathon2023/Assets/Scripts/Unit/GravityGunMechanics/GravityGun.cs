<<<<<<< Updated upstream
=======
﻿using Services.Input;
using System;
using System.Collections;
using Unit.Portal;
using Unit.Weapon;
>>>>>>> Stashed changes
using UnityEngine;

namespace Unit.GravityGunMechanics
{
    public class GravityGun : MonoBehaviour
    {
        [SerializeField] private float _catchDistance;
        [SerializeField] private float _catchPower;
        [SerializeField] private float _dropPower;
        [SerializeField] private Transform _pointGravity;

        private Rigidbody _currentRigidbody;

        public void Fire() 
        {
<<<<<<< Updated upstream
            if (_currentRigidbody == null) 
=======
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
>>>>>>> Stashed changes
            {
                var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)); //Center the screen in the crosshairs

                if (!Physics.Raycast(ray, out var hit, _catchDistance))
                {
                    return;
                }
                
                if (hit.collider.gameObject.layer != 11)
                {
                    return; //11 - InteractiveObjectForGravity
                }
                
                _currentRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
                
                if (_currentRigidbody == null)
                {
                    Debug.LogError("InteractiveObjectForGravity does not have a Rigidbody");
                }
            } 
            else 
            {
                DragIn();
            }            
        }

        //When the pressed button "Fire" is released
        public void Release() => _currentRigidbody = null;

        //On a different "Fire" button
        public void Drop() 
        {
            _currentRigidbody.velocity = _pointGravity.forward * _dropPower;
            Release();
        }

<<<<<<< Updated upstream
        private void DragIn() 
            => _currentRigidbody.velocity = (_pointGravity.position - (_currentRigidbody.transform.position + _currentRigidbody.centerOfMass)) * _catchPower;        
=======
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
>>>>>>> Stashed changes
    }
}
