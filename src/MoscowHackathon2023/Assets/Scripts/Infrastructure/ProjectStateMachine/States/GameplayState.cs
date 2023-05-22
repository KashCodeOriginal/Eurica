using Data.StaticData.LevelData;
using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories.UIFactory;
using Services.Input;
using UI.GameplayScreen;
using Unit.WeaponInventory;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<Bootstrap>, IEnterableWithOneArg<LevelData>
    {
        private readonly IUIFactory _uiFactory;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly IGameInstancesContainer _gameInstancesContainer;
        public Bootstrap Initializer { get; }
        
        private Inventory _inventory;

        public GameplayState(Bootstrap initializer,
            IUIFactory uiFactory, 
            PlayerInputActionReader playerInputActionReader,
            IGameInstancesContainer gameInstancesContainer)
        {
            _uiFactory = uiFactory;
            _playerInputActionReader = playerInputActionReader;
            _gameInstancesContainer = gameInstancesContainer;
            Initializer = initializer;
        }

        public async void OnEnter(LevelData levelData)
        {
            var gameplayScreen = await _uiFactory.CreateGameplayScreen();
            
            var gameplayScreenComponent = gameplayScreen.GetComponent<GameplayScreen>();
            
            /*_portalGun = _gunFactory.CreatePortalGun();
            _gravityGun = _gunFactory.CreateGravityGun();
            _scaleGun = _gunFactory.CreateScaleGun();*/

            _inventory = new Inventory(_uiFactory, _playerInputActionReader, gameplayScreenComponent.InventoryTransform);

            await _inventory.ShowPanel();
            
            if (levelData.IsPlayerInstancingAtStart)
            {
                _gameInstancesContainer.TurnOnPlayer();
            }
        }
    }
}