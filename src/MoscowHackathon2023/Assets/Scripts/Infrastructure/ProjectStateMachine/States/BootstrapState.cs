using Zenject;
using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories.GunsFactory;
using Services.Factories.UIFactory;
using Services.Input;
using Services.StaticData;
using Unit.WeaponInventory;

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IState<Bootstrap>, IInitializable
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly IGameInstancesContainer _gameInstancesContainer;
        private readonly IGunFactory _gunFactory;

        public BootstrapState(Bootstrap initializer, 
            IStaticDataService staticDataService,
            IUIFactory uiFactory,
            PlayerInputActionReader playerInputActionReader,
            IGameInstancesContainer gameInstancesContainer,
            IGunFactory gunFactory)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
            _playerInputActionReader = playerInputActionReader;
            _gameInstancesContainer = gameInstancesContainer;
            _gunFactory = gunFactory;
            Initializer = initializer;
        }

        public Bootstrap Initializer { get; }

        public void Initialize()
        {
            _staticDataService.LoadStaticData();

            var inventory = new Inventory(_uiFactory, _playerInputActionReader, _gameInstancesContainer);
            
            var portalGun = _gunFactory.CreatePortalGun();
            var gravityGun = _gunFactory.CreateGravityGun();
            var scaleGun = _gunFactory.CreateScaleGun();

            inventory.AddWeaponToInventory(portalGun);
            inventory.AddWeaponToInventory(gravityGun);
            inventory.AddWeaponToInventory(scaleGun);

            _gameInstancesContainer.SetUpInventory(inventory);
            
            Initializer.StateMachine.SwitchState<MenuLoadingState>();
        }
    }
}