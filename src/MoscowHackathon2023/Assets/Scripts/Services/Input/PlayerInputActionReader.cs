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

        public Action<Vector2> OnMousePositionInput;

        public Action IsLeftButtonClicked;
        public Action IsRightButtonClicked;

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
            OnMousePositionInput?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnMouseLeftButtonClick(InputAction.CallbackContext context)
        {
            IsLeftButtonClicked?.Invoke();
        }

        public void OnMouseRightButtonClick(InputAction.CallbackContext context)
        {
            IsRightButtonClicked?.Invoke();
        }
    }
}