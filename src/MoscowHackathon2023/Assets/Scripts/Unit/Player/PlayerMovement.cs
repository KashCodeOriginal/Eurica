using System.Linq;
using Data.StaticData.PlayerData;
using UnityEngine;
using Services.Input;
using Services.PlaySounds;

namespace Unit.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isRunning;
        private bool _pressingJump;
        private bool _isGrounded;
        private bool _isJumping;

        private float _currentSpeed;

        private Camera _camera;

        private Vector2 _currentDirection;

        private PlayerInputActionReader _playerInputActionReader;

        private PlayerBaseSettings _playerSettings;

        private IPlaySoundsService _playSoundsService;

        private float _stepSoundTimer;

        public void Construct(PlayerInputActionReader playerInputActionReader, 
            Camera camera, 
            PlayerBaseSettings playerSettings,
            IPlaySoundsService playSoundsService)
        {
            _playSoundsService = playSoundsService;
            
            _playerInputActionReader = playerInputActionReader;

            _playerSettings = playerSettings;

            _camera = camera;

            _playerInputActionReader.OnMovementInput += OnMovementInput;

            _playerInputActionReader.IsPlayerAccelerationButtonClickStarted += OnPlayerRun;
            _playerInputActionReader.IsPlayerAccelerationButtonClickEnded += OnPlayerWalk;

            _playerInputActionReader.IsPlayerJumpButtonClicked += OnPlayerJump;

            _stepSoundTimer = _playerSettings.WalkSoundFrequency;
        }

        private void FixedUpdate()
        {
            if (IsCameraInactive())
            {
                return;
            }

            UpdateCurrentSpeed();

            if (_pressingJump && IsGrounded())
            {
                Jump();
            }

            _pressingJump = false;

            MovePlayer();
        }

        private void MovePlayer()
        {
            var newDirection = new Vector3(_currentDirection.x, 0f, _currentDirection.y).normalized;
            newDirection = _camera.transform.forward * newDirection.z + _camera.transform.right * newDirection.x;
            newDirection.y = 0f;

            Vector3 desiredVelocity = newDirection.normalized * _currentSpeed;
            Vector3 slopeVelocity = Vector3.ProjectOnPlane(desiredVelocity, Vector3.up);

            if (slopeVelocity.sqrMagnitude > 0.0001f)
            {
                if (_isGrounded)
                {
                    _stepSoundTimer += Time.deltaTime;
                }

                if (_stepSoundTimer >= _playerSettings.WalkSoundFrequency)
                {
                    PlayStepsSound();

                    _stepSoundTimer = 0;
                }

                _rigidbody.MovePosition(_rigidbody.position + slopeVelocity * Time.fixedDeltaTime);
            }
        }

        private void Jump()
        {
            _isJumping = true;

            PlayStepsSound();

            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

            _rigidbody.AddForce(new Vector3(0f, _playerSettings.JumpPower, 0f), ForceMode.Impulse);
        }

        private void PlayStepsSound()
        {
            _playSoundsService.PlayAudioClip(_playerSettings.WalkSound, VolumeLevel.StepsVolume, true, minPitch: 0.95f, maxPitch: 1.05f);
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
            _pressingJump = isJumping;
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

            if (_isRunning && !_isJumping)
            {
                _currentSpeed *= _playerSettings.RunMultiplier;
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

        private void OnDestroy()
        {
            if (_playerInputActionReader == null)
            {
                return;
            }

            _playerInputActionReader.OnMovementInput -= OnMovementInput;

            _playerInputActionReader.IsPlayerAccelerationButtonClickStarted -= OnPlayerRun;
            _playerInputActionReader.IsPlayerAccelerationButtonClickEnded -= OnPlayerWalk;
            
            _playerInputActionReader.IsPlayerJumpButtonClicked -= OnPlayerJump;
        }
    }
}
