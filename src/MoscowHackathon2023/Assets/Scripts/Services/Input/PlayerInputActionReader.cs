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
    }
}