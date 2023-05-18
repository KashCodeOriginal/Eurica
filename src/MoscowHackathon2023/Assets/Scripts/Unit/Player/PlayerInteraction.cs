using Services.Input;
using Unit.Base;
using UnityEngine;
using Zenject;

namespace Unit.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Inject]
        public void Construct(PlayerInputActionReader playerInputActionReader)
        {
            _playerInputActionReader = playerInputActionReader;

            _playerInputActionReader.IsPlayerInteractionButtonClicked += IsPlayerInteract;
        }
        
        private bool _canInteract;

        private PlayerInputActionReader _playerInputActionReader;

        private void Update()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            if (!hit.collider.TryGetComponent(out IInteractable interactable))
            {
                return;
            }

            if (_canInteract)
            {
                interactable.Interact();
            }
        }

        private void IsPlayerInteract(bool condition)
        {
            _canInteract = condition;
        }

        private void OnDisable()
        {
            _playerInputActionReader.IsPlayerInteractionButtonClicked -= IsPlayerInteract;
        }
    }
}