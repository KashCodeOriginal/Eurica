﻿using Services.Containers;
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

        private bool _canInteract;

        private PlayerInputActionReader _playerInputActionReader;

        private IGameInstancesContainer _gameInstancesContainer;

        private void Update()
        {
            if (_gameInstancesContainer == null)
            {
                return;
            }
            
            var ray = _gameInstancesContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

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

        private void OnDestroy()
        {
            _playerInputActionReader.IsPlayerInteractionButtonClicked -= IsPlayerInteract;
        }
    }
}