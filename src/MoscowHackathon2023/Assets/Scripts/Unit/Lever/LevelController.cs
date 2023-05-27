using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Lever
{
    public class LeverController : MonoBehaviour
    {
        [SerializeField] private LeverButtonLogic _leverButtonLogic;
        [SerializeField] private GameObject _movingObject;

        [SerializeField] private Vector3 _hidePosition;
        [SerializeField] private Vector3 _releasePosition;

        [SerializeField] private float _releaseSpeed;

        [SerializeField] private List<Rigidbody> _movingRigidbodies;

        private bool _released;

        private Vector3 _targetPosition;

        private void OnEnable()
        {
            if (_leverButtonLogic == null)
            {
                return;
            }
            
            _leverButtonLogic.OnPress.AddListener(ChangeLevelPartState);
        }

        private void Start()
        {
            Hide();
        }

        private void Update()
        {
            var movingObjectPosition = _movingObject.transform.position;

            movingObjectPosition =
                new Vector3(Mathf.Round(movingObjectPosition.x),
                    Mathf.Round(movingObjectPosition.y),
                    Mathf.Round(movingObjectPosition.z));

            if (movingObjectPosition == _targetPosition)
            {
                if (_released)
                {
                    foreach (var movingRigidbody in _movingRigidbodies)
                    {
                        movingRigidbody.isKinematic = false;
                    }
                }

                return;
            }
            
            

            _movingObject.transform.position = Vector3.Lerp(_movingObject.transform.position,
                _targetPosition, _releaseSpeed * Time.deltaTime);
        }

        public void ChangeLevelPartState()
        {
            if (!_released)
            {
                Release();
                _released = true;
                
                return;
            }
            
            Hide();
            _released = false;
        }

        private void Release()
        {
            _targetPosition = _releasePosition;
        }

        private void Hide()
        {
            _targetPosition = _hidePosition;
            
            foreach (var movingRigidbody in _movingRigidbodies)
            {
                movingRigidbody.isKinematic = true;
            }
        }
        
        private void OnDisable()
        {
            if (_leverButtonLogic == null)
            {
                return;
            }
            
            _leverButtonLogic.OnPress.RemoveListener(ChangeLevelPartState);
        }
    }
}
