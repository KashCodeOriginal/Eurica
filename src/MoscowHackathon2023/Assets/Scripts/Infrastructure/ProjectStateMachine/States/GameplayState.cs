using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories.GunsFactory;
using Services.Factories.UIFactory;
using Services.GameProgress;
using Services.Input;
using Services.PlaySounds;
using UI.GameplayScreen;
using Unit.UniversalGun;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<Bootstrap>, IEnterable
    {
        private readonly IUIFactory _uiFactory;
        private readonly IGameInstancesContainer _gameInstancesContainer;
        private readonly IGunFactory _gunFactory;
        private readonly IGameProgressService _gameProgressService;
        private readonly IPlaySoundsService _playSoundsService;

        public Bootstrap Initializer { get; }
        
        public GameplayState(Bootstrap initializer,
            IUIFactory uiFactory, 
            IGameInstancesContainer gameInstancesContainer,
            IGunFactory gunFactory,
            IGameProgressService gameProgressService,
            IPlaySoundsService playSoundsService)
        {
            _uiFactory = uiFactory;
            _gameInstancesContainer = gameInstancesContainer;
            _gunFactory = gunFactory;
            _gameProgressService = gameProgressService;
            Initializer = initializer;
            _playSoundsService = playSoundsService;
        }

        public async void OnEnter()
        {
            _uiFactory.DestroyGameLoadingScreen();

            var gameplayScreen = await _uiFactory.CreateGameplayScreen();
            
            //gameplayScreen.SetActive(false);

            var universalGunView = await _gunFactory.CreateUniversalGunView();
            
            universalGunView.Construct(_gameProgressService, _playSoundsService);

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

        }
    }
}