using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{
    [CreateAssetMenu(fileName = "PlayerInputActionReader", menuName = "Input/PlayerInputActionReader", order = 0)]
    public class PlayerInputActionReader : ScriptableObject, PlayerInputAction.IPlayerInputActions
    {
        private PlayerInputAction _playerInputAction;

        public Action<Vector2> OnMovementInput;
        
        public Vector2 OnMousePositionInput { get; private set; }

        public Action IsLeftButtonClicked;
        public Action IsRightButtonClicked;

        public Action IsLeftButtonClickStarted;
        public Action IsLeftButtonClickEnded;
        
        public Action IsRightButtonClickStarted;
        public Action IsRightButtonClickEnded;
        
        public Action IsPlayerAccelerationButtonClickStarted;
        public Action IsPlayerAccelerationButtonClickEnded;
        
        public Action IsPlayerCrouchButtonClickStarted;
        public Action IsPlayerCrouchButtonClickEnded;
        
        public Action<bool> IsPlayerInteractionButtonClicked;

        public Action<float> IsMouseScroll;

        public Action<bool> IsPlayerJumpButtonClicked;

        private void OnEnable()
        {
            if (_playerInputAction != null)
            {
                return;
            }

            _playerInputAction = new PlayerInputAction();
            
            _playerInputAction.PlayerInput.SetCallbacks(this);
            _playerInputAction.Enable();
        }

        public void OnPlayerInput(InputAction.CallbackContext context)
        {
            OnMovementInput?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            OnMousePositionInput = context.ReadValue<Vector2>();
        }

        public void OnMouseLeftButtonClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsLeftButtonClickStarted?.Invoke();
            }
            
            if (context.performed)
            {
                IsLeftButtonClicked?.Invoke();
            }
            
            if (context.canceled)
            {
                IsLeftButtonClickEnded?.Invoke();
            }
        }

        public void OnMouseRightButtonClick(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsRightButtonClickStarted?.Invoke();
            }
            
            if (context.performed)
            {
                IsRightButtonClicked?.Invoke();
            }
            
            if (context.canceled)
            {
                IsRightButtonClickEnded?.Invoke();
            }
        }

        public void OnMouseWheelScroll(InputAction.CallbackContext context)
        {
            IsMouseScroll?.Invoke(context.ReadValue<float>()); ;
        }

        public void OnPlayerInteraction(InputAction.CallbackContext context)
        {
            IsPlayerInteractionButtonClicked?.Invoke(context.performed);
        }

        public void OnPlayerAcceleration(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsPlayerAccelerationButtonClickStarted?.Invoke();
            }

            if (context.canceled)
            {
                IsPlayerAccelerationButtonClickEnded?.Invoke();
            }
        }

        public void OnPlayerJump(InputAction.CallbackContext context)
        {
            IsPlayerJumpButtonClicked?.Invoke(context.performed);
        }

        public void OnPlayerCrouch(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsPlayerCrouchButtonClickStarted?.Invoke();
            }

            if (context.canceled)
            {
                IsPlayerCrouchButtonClickEnded?.Invoke();
            }
        }
    }
}