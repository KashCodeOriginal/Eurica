using Data.StaticData.LevelData;
using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories.GunsFactory;
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
        private readonly IGunFactory _gunFactory;
        
        public Bootstrap Initializer { get; }
        
        private Inventory _inventory;

        public GameplayState(Bootstrap initializer,
            IUIFactory uiFactory, 
            PlayerInputActionReader playerInputActionReader,
            IGameInstancesContainer gameInstancesContainer,
            IGunFactory gunFactory)
        {
            _uiFactory = uiFactory;
            _playerInputActionReader = playerInputActionReader;
            _gameInstancesContainer = gameInstancesContainer;
            _gunFactory = gunFactory;
            Initializer = initializer;
        }

        public async void OnEnter(LevelData levelData)
        {
            var gameplayScreen = await _uiFactory.CreateGameplayScreen();
            
            var gameplayScreenComponent = gameplayScreen.GetComponent<GameplayScreen>();
            
            var portalGun = _gunFactory.CreatePortalGun();
            var gravityGun = _gunFactory.CreateGravityGun();
            var scaleGun = _gunFactory.CreateScaleGun();

            _inventory = new Inventory(_uiFactory, _playerInputActionReader, gameplayScreenComponent.InventoryTransform);

            await _inventory.ShowPanel();
            
            _inventory.CollectWeapon(portalGun);
            _inventory.CollectWeapon(gravityGun);
            _inventory.CollectWeapon(scaleGun);
            
            if (levelData.IsPlayerInstancingAtStart)
            {
                _gameInstancesContainer.TurnOnPlayer();
            }
        }
    }
}