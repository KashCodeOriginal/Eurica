using Cinemachine;
using Data.AssetsAddressablesConstants;
using Infrastructure.ProjectStateMachine.Base;
using Services.Factories.AbstractFactory;
using Services.Input;
using Unit.Player;
using Unit.ScaleGun;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameSetUpState : IState<Bootstrap>, IEnterable
    {
        public Bootstrap Initializer { get; }
        
        private readonly IAbstractFactory _abstractFactory;
        private readonly PlayerInputActionReader _playerInputActionReader;

        public GameSetUpState(Bootstrap initializer,
            IAbstractFactory abstractFactory,
            PlayerInputActionReader playerInputActionReader)
        {
            _abstractFactory = abstractFactory;
            _playerInputActionReader = playerInputActionReader;
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
            
            ScaleGun scaleGun = new ScaleGun(_playerInputActionReader, mainCamera);

            if (playerInstance.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.Construct(_playerInputActionReader, mainCamera.transform);
            }
        }
    }
}