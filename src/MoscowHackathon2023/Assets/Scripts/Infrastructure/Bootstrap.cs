using Data.StaticData.PlayerData;
using Infrastructure.ProjectStateMachine.Base;
using Infrastructure.ProjectStateMachine.States;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Services.Factories.GunsFactory;
using Services.Factories.PortalFactory;
using Services.Factories.UIFactory;
using Services.GameProgress;
using Services.Input;
using Services.PlaySounds;
using Services.StaticData;
using Unit.ScaleGun;
using Unit.WeaponInventory;
using UnityEditor;

namespace Infrastructure
{
    public class Bootstrap
    {
        public Bootstrap(IUIFactory uiFactory,
            IAbstractFactory abstractFactory,
            PlayerInputActionReader playerInputActionReader,
            PlayerBaseSettings playerSettings,
            IGunFactory gunFactory,
            IGameInstancesContainer gameInstancesContainer,
            IStaticDataService staticDataService,
            IPlaySoundsService playSoundsService,
            IGameProgressService gameProgressService)
        {
            StateMachine = new StateMachine<Bootstrap>(
                new BootstrapState(this, 
                    staticDataService, 
                    uiFactory,
                    playerInputActionReader,
                    gameInstancesContainer,
                    gunFactory),
                new MenuLoadingState(this, uiFactory),
                new MainMenuState(this, uiFactory),
                new GameLoadingState(this, uiFactory),
                new GameSetUpState(this, gunFactory,
                    abstractFactory, playerInputActionReader, playerSettings,
                    gameInstancesContainer, staticDataService, playSoundsService, uiFactory,
                    gameProgressService),
                new GameplayState(this, uiFactory, gameInstancesContainer,gunFactory, gameProgressService));  
            
                StateMachine.SwitchState<BootstrapState>();
        }

        public readonly StateMachine<Bootstrap> StateMachine;
    }
}
