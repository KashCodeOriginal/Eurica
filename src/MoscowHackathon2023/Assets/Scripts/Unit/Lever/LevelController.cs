using System;
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

        private bool _released;

        private Vector3 _targetPosition;

        private void OnEnable()
        {
            _leverButtonLogic.OnPress.AddListener(ChangeLevelPartState);
        }

        private void Start()
        {
            _targetPosition = _hidePosition;
        }

        private void Update()
        {
            if (_movingObject.transform.position == _targetPosition)
                return;

            _movingObject.transform.position = Vector3.Lerp(_movingObject.transform.position,
                _targetPosition, _releaseSpeed * Time.deltaTime);
        }

        private void ChangeLevelPartState()
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
        }
        
        private void OnDisable()
        {
            _leverButtonLogic.OnPress.RemoveListener(ChangeLevelPartState);
        }
    }
}
