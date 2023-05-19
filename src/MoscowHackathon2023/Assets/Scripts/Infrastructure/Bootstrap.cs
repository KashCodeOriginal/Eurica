using Data.StaticData.PlayerData;
using Infrastructure.ProjectStateMachine.Base;
using Infrastructure.ProjectStateMachine.States;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Services.Factories.GunsFactory;
using Services.Factories.PortalFactory;
using Services.Factories.UIFactory;
using Services.Input;
using Unit.ScaleGun;
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
            ICameraContainer cameraContainer,
            IPlayerContainer playerContainer)
        {
            StateMachine = new StateMachine<Bootstrap>(
                new BootstrapState(this),
                new MenuLoadingState(this, uiFactory),
                new MainMenuState(this, uiFactory),
                new GameLoadingState(this),
                new GameSetUpState(this, gunFactory,
                    abstractFactory, playerInputActionReader, 
                    cameraContainer, playerSettings, uiFactory, 
                    playerContainer),
                new GameplayState(this, uiFactory));  
            
                StateMachine.SwitchState<BootstrapState>();
        }

        public readonly StateMachine<Bootstrap> StateMachine;
    }
}
