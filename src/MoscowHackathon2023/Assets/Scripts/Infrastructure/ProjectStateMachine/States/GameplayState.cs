using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories.GunsFactory;
using Services.Factories.UIFactory;
using Services.Input;
using UI.GameplayScreen;
using Unit.UniversalGun;

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

            if (_gameInstancesContainer.CollectedGunViews.Contains(GunTypes.Portal))
            {
                _gameInstancesContainer.Inventory.Weapons[0].SetUpUniversalView(universalGunView);
            }
            if (_gameInstancesContainer.CollectedGunViews.Contains(GunTypes.Gravity))
            {
                _gameInstancesContainer.Inventory.Weapons[1].SetUpUniversalView(universalGunView);
            }
            if (_gameInstancesContainer.CollectedGunViews.Contains(GunTypes.Scale))
            {
                _gameInstancesContainer.Inventory.Weapons[2].SetUpUniversalView(universalGunView);
            }

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