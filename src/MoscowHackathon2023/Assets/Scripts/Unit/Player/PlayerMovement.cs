using UnityEngine;
using Services.Input;

namespace Unit.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed;

        private Vector2 _currentDirection;
        
        private PlayerInputActionReader _playerInputActionReader;

        public void Construct(PlayerInputActionReader playerInputActionReader)
        {
            _playerInputActionReader = playerInputActionReader;

            _playerInputActionReader.OnMovementInput += OnMovementInput;
            
            
        }

        private void Update()
        {
            var newDirection = new Vector3(_currentDirection.x, 0f, _currentDirection.y).normalized;

            newDirection += Physics.gravity;
            
            _characterController.Move(newDirection * (_speed * Time.deltaTime));
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
