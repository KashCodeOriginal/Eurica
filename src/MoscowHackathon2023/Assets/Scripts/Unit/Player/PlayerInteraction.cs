using Services.Containers;
using Services.Input;
using Unit.Base;
using UnityEngine;
using Zenject;

namespace Unit.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        public void Construct(PlayerInputActionReader playerInputActionReader, 
            IGameInstancesContainer cameraContainer)
        {
            _playerInputActionReader = playerInputActionReader;

            _gameInstancesContainer = cameraContainer;

            _playerInputActionReader.IsPlayerInteractionButtonClicked += IsPlayerInteract;
        }

        private bool _pressingInteract;

        private PlayerInputActionReader _playerInputActionReader;

        private IGameInstancesContainer _gameInstancesContainer;

        private void Update()
        {
            if (_gameInstancesContainer == null)
            {
                return;
            }

            var maxDistance = 5f;
            var ray = _gameInstancesContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (!Physics.Raycast(ray, out var hit, maxDistance))
            {
                return;
            }

            if (!hit.collider.TryGetComponent(out IInteractable interactable))
            {
                return;
            }

            if (_pressingInteract)
            {
                interactable.Interact();
            }

            _pressingInteract = false;
        }

        private void IsPlayerInteract(bool condition)
        {
            _pressingInteract = condition;
        }

        private void OnDestroy()
        {
            _playerInputActionReader.IsPlayerInteractionButtonClicked -= IsPlayerInteract;
        }
    }
}