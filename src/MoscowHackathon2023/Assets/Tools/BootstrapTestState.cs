using Zenject;
using Infrastructure.ProjectStateMachine.Base;

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapTestState : IState<BootstrapTest>, IInitializable
    {
        public BootstrapTest Initializer { get; }

        public BootstrapTestState(BootstrapTest initializer)
        {
            Initializer = initializer;
        }

        public void Initialize()
        {
            
        }
    }
}