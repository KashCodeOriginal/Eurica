using Infrastructure.ProjectStateMachine.Base;
using Infrastructure.ProjectStateMachine.States;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;
using Services.Input;
using Unit.ScaleGun;

namespace Infrastructure
{
    public class Bootstrap
    {
        public Bootstrap(IUIFactory uiFactory,
            IAbstractFactory abstractFactory,
            PlayerInputActionReader playerInputActionReader)
        {
            StateMachine = new StateMachine<Bootstrap>(
                new BootstrapState(this),
                new MenuLoadingState(this, uiFactory),
                new MainMenuState(this, uiFactory),
                new GameLoadingState(this),
<<<<<<< Updated upstream
                new GameSetUpState(this,
                    abstractFactory, playerInputActionReader),
                new GameplayState(this, uiFactory));

=======
                new GameSetUpState(this, abstractFactory, playerInputActionReader),
                new TestState(),
                new GameplayState(this, uiFactory));  
>>>>>>> Stashed changes
                StateMachine.SwitchState<BootstrapState>();
        }

        public readonly StateMachine<Bootstrap> StateMachine;
    }
}
