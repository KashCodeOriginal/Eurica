using Infrastructure.ProjectStateMachine.Base;
using Infrastructure.ProjectStateMachine.States;
using Services.Factories.UIFactory;

namespace Infrastructure
{
    public class Bootstrap
    {
        public Bootstrap(IUIFactory uiFactory)
        {
            StateMachine = new StateMachine<Bootstrap>(
                new BootstrapState(this),
                new MenuLoadingState(this, uiFactory),
                new MainMenuState(this, uiFactory),
                new GameLoadingState(this),
                new GameSetUpState(this),
                new GameplayState(this, uiFactory));

                StateMachine.SwitchState<BootstrapState>();
        }

        public readonly StateMachine<Bootstrap> StateMachine;
    }
}
