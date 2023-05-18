using UnityEngine;
using Services.Input;

namespace Unit.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        private Transform _cameraTransform;

        private Vector2 _currentDirection;
        
        private PlayerInputActionReader _playerInputActionReader;

        public void Construct(PlayerInputActionReader playerInputActionReader, Transform cameraTransform)
        {
            _playerInputActionReader = playerInputActionReader;

            _cameraTransform = cameraTransform;

            _playerInputActionReader.OnMovementInput += OnMovementInput;
        }

        private void Update()
        {
            if (_cameraTransform == null)
            {
                return;
            }
            
            var newDirection = new Vector3(_currentDirection.x, 0f, _currentDirection.y).normalized;

            newDirection = _cameraTransform.forward * newDirection.z + _cameraTransform.right * newDirection.x;

            newDirection.y = 0f;

            _rigidbody.AddForce(newDirection * _speed, ForceMode.Impulse);
        }

        private void OnMovementInput(Vector2 direction)
        {
            _currentDirection = direction;
        }

        private void OnDisable()
        {
            if (_playerInputActionReader != null)
            {
                _playerInputActionReader.OnMovementInput -= OnMovementInput;
            }
        }
    }
}
