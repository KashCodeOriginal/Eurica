using Zenject;
using Infrastructure.ProjectStateMachine.Base;

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IState<Bootstrap>, IInitializable
    {
        public Bootstrap Initializer { get; }

        public BootstrapState(Bootstrap initializer)
        {
            Initializer = initializer;
        }

        public void Initialize()
        {
            Initializer.StateMachine.SwitchState<MenuLoadingState>();
        }
    }
}