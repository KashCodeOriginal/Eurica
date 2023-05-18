using Infrastructure.ProjectStateMachine.Base;
using Services.Factories.UIFactory;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<Bootstrap>, IEnterable
    {
        private readonly IUIFactory _uiFactory;
        public Bootstrap Initializer { get; }

        public GameplayState(Bootstrap initializer, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Initializer = initializer;
        }

        public void OnEnter()
        {
            _uiFactory.CreateGameplayScreen();
        }
    }
}