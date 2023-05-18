using Cinemachine;
using Data.AssetsAddressablesConstants;
using Data.StaticData.PlayerData;
using Infrastructure.ProjectStateMachine.Base;
using Services.Factories.AbstractFactory;
using Services.Input;
using Unit.Player;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameSetUpState : IState<Bootstrap>, IEnterable
    {
        public Bootstrap Initializer { get; }
        
        private readonly IAbstractFactory _abstractFactory;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly PlayerBaseSettings _playerSettings;

        public GameSetUpState(Bootstrap initializer,
            IAbstractFactory abstractFactory,
            PlayerInputActionReader playerInputActionReader,
            PlayerBaseSettings playerSettings)
        {
            _abstractFactory = abstractFactory;
            _playerInputActionReader = playerInputActionReader;
            _playerSettings = playerSettings;
            Initializer = initializer;
        }

        public async void OnEnter()
        {
            var playerInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PLAYER_PREFAB);

            var cameraInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.CAMERA_PREFAB);

            SetUp(playerInstance, cameraInstance);

            Initializer.StateMachine.SwitchState<GameplayState>();
        }

        private void SetUp(GameObject playerInstance, GameObject cameraInstance)
        {
            var virtualCamera = cameraInstance.GetComponentInChildren<CinemachineVirtualCamera>();
            
            var mainCamera = cameraInstance.GetComponentInChildren<Camera>();
            
            virtualCamera.Follow = playerInstance.transform;
            
            if (playerInstance.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.Construct(_playerInputActionReader, mainCamera, _playerSettings);
            }
        }
    }
}