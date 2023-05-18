using Data.AssetsAddressablesConstants;
using Infrastructure.ProjectStateMachine.Base;
using Services.Factories.AbstractFactory;
using Services.Input;
using Unit.Player;
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

            SetUp(playerInstance);
            
            Initializer.StateMachine.SwitchState<GameplayState>();
        }

        private void SetUp(GameObject playerInstance)
        {
            if (playerInstance.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.Construct(_playerInputActionReader);
            }
        }
    }
}