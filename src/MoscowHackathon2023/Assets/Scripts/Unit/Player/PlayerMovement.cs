using System;
using System.Linq;
using Data.StaticData.PlayerData;
using UnityEngine;
using Services.Input;

namespace Unit.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _fullStandingCollider;
        [SerializeField] private Collider _crouchingCollider;

        private bool _isRunning;
        private bool _canRun;
        private bool _canJump;
        private bool _isCrouching;
        private bool _isGrounded;
        private bool _isJumping;

        [SerializeField] private float _currentStamina;

        private float _currentSpeed;

        private Camera _camera;

        private Vector2 _currentDirection;

        private PlayerInputActionReader _playerInputActionReader;

        private PlayerBaseSettings _playerSettings;

        public void Construct(PlayerInputActionReader playerInputActionReader, Camera camera, PlayerBaseSettings playerSettings)
        {
            _playerInputActionReader = playerInputActionReader;

            _playerSettings = playerSettings;

            _camera = camera;

            _playerInputActionReader.OnMovementInput += OnMovementInput;

            _playerInputActionReader.IsPlayerAccelerationButtonClickStarted += OnPlayerRun;
            _playerInputActionReader.IsPlayerAccelerationButtonClickEnded += OnPlayerWalk;

            _playerInputActionReader.IsPlayerJumpButtonClicked += OnPlayerJump;

            _playerInputActionReader.IsPlayerCrouchButtonClickStarted += OnPlayerCrouch;
            _playerInputActionReader.IsPlayerCrouchButtonClickEnded += OnPlayerCrouchEnded;
            
            _currentStamina = _playerSettings.Stamina;
        }
        
        private void Update()
        {
            if (_playerSettings == null)
            {
                return;
            }
            
            if (!_canRun)
            {
                _canRun = _currentStamina > _playerSettings.MinStaminaToRun;
            }
            else
            {
                _canRun = _isRunning && _currentStamina > 0;
            }

            if (_currentDirection.magnitude < 0.5f)
            {
                _isRunning = false;
            }

            if (!_isRunning && _currentStamina <= _playerSettings.Stamina)
            {
                _currentStamina += _playerSettings.StaminaRecovery * Time.deltaTime;
            }

            if (_isRunning && _canRun && !_isJumping)
            {
                _currentStamina -= _playerSettings.StaminaWaste * Time.deltaTime;
            }

            _currentStamina = Mathf.Clamp(_currentStamina, 0f, _playerSettings.Stamina);
        }

        private void FixedUpdate()
        {
            if (IsCameraInactive())
            {
                return;
            }

            UpdateCurrentSpeed();

            if (_canJump && IsGrounded())
            {
                Jump();
            }

            MovePlayer();
        }

        private void MovePlayer()
        {
            var newDirection = new Vector3(_currentDirection.x, 0f, _currentDirection.y).normalized;

            newDirection = _camera.transform.forward * newDirection.z + _camera.transform.right * newDirection.x;

            newDirection.y = 0f;

            _rigidbody.AddForce(newDirection.normalized * _currentSpeed, ForceMode.Impulse);
        }

        private void Jump()
        {
            _isJumping = true;
            
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

            _rigidbody.AddForce(new Vector3(0f, _playerSettings.JumpPower, 0f), ForceMode.Impulse);
        }

        private bool IsCameraInactive()
        {
            return _camera == null;
        }

        private void OnMovementInput(Vector2 direction)
        {
            _currentDirection = direction;
        }

        private void OnPlayerRun()
        {
            _isRunning = true;
        }

        private void OnPlayerWalk()
        {
            _isRunning = false;
        }

        private void OnPlayerJump(bool isJumping)
        {
            _canJump = isJumping;
        }

        private void OnPlayerCrouch()
        {
            _isCrouching = true;

            _fullStandingCollider.enabled = false;
            _crouchingCollider.enabled = true;
        }

        private void OnPlayerCrouchEnded()
        {
            _isCrouching = false;

            _fullStandingCollider.enabled = true;
            _crouchingCollider.enabled = false;
        }

        private void UpdateCurrentSpeed()
        {
            if (_currentDirection.x is > 0 or < 0)
            {
                _currentSpeed = _playerSettings.SideWalkSpeed;
            }

            if (_currentDirection.y < 0)
            {
                _currentSpeed = _playerSettings.BackwardWalkSpeed;
            }

            if (_currentDirection.y > 0)
            {
                _currentSpeed = _playerSettings.ForwardWalkSpeed;
            }

            if (_isRunning && !_isCrouching && _canRun && !_isJumping)
            {
                _currentSpeed *= _playerSettings.RunMultiplier;
            }

            if (_isCrouching)
            {
                _currentSpeed = _playerSettings.CrouchWalkSpeed;
            }
        }

        #region Ground Detection

        private bool IsGrounded()
        {
            return _isGrounded;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!collision.contacts.Any(contact => Vector3.Dot(contact.normal, Vector3.up) > 0.7f))
            {
                return;
            }
            
            _isJumping = false;
            _isGrounded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            _isGrounded = false;
        }

        #endregion

        private void OnDisable()
        {
            if (_playerInputActionReader == null)
            {
                return;
            }

            _playerInputActionReader.OnMovementInput -= OnMovementInput;

            _playerInputActionReader.IsPlayerAccelerationButtonClickStarted -= OnPlayerRun;
            _playerInputActionReader.IsPlayerAccelerationButtonClickEnded -= OnPlayerWalk;
        }
    }
}
