using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories.GunsFactory;
using Services.Factories.UIFactory;
using Services.Input;
using UI.GameplayScreen;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<Bootstrap>, IEnterable
    {
        private readonly IUIFactory _uiFactory;
        private readonly IGameInstancesContainer _gameInstancesContainer;
        private readonly IGunFactory _gunFactory;
        
        public Bootstrap Initializer { get; }
        
        public GameplayState(Bootstrap initializer,
            IUIFactory uiFactory, 
            IGameInstancesContainer gameInstancesContainer,
            IGunFactory gunFactory)
        {
            _uiFactory = uiFactory;
            _gameInstancesContainer = gameInstancesContainer;
            _gunFactory = gunFactory;
            Initializer = initializer;
        }

        public async void OnEnter()
        {
            var gameplayScreen = await _uiFactory.CreateGameplayScreen();
            
            var gameplayScreenComponent = gameplayScreen.GetComponent<GameplayScreen>();
            
            var universalGunView = await _gunFactory.CreateUniversalGunView();
            
            _gameInstancesContainer.SetUpUniversalGunView(universalGunView);

            //await _gameInstancesContainer.Inventory.ShowPanel(gameplayScreenComponent.InventoryTransform);

            _gameInstancesContainer.TurnOnPlayerUI();

            /*_gameInstancesContainer.Inventory.DisplayGunViewInInventory
                (_gameInstancesContainer.Inventory.Weapons[0]);
            
            _gameInstancesContainer.Inventory.DisplayGunViewInInventory
                (_gameInstancesContainer.Inventory.Weapons[1]);
            
            _gameInstancesContainer.Inventory.DisplayGunViewInInventory
                (_gameInstancesContainer.Inventory.Weapons[2]);*/

        }
    }
}