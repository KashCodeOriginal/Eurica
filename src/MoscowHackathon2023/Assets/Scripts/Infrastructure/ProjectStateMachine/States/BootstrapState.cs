using Zenject;
using Infrastructure.ProjectStateMachine.Base;
using Services.StaticData;

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IState<Bootstrap>, IInitializable
    {
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(Bootstrap initializer, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            Initializer = initializer;
        }

        public Bootstrap Initializer { get; }

        public void Initialize()
        {
            _staticDataService.LoadStaticData();
            
            Initializer.StateMachine.SwitchState<MenuLoadingState>();
        }
    }
}