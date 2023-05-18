using Infrastructure.ProjectStateMachine.Base;
using Infrastructure.ProjectStateMachine.States;
using Services.Factories;
using Tools;
using Unit;

namespace Infrastructure
{
    public class BootstrapTest
    {
        public BootstrapTest(
            GunFactory gunFactory)
        {
            StateMachine = new StateMachine<BootstrapTest>(                
                new LoadTestState(gunFactory));

                StateMachine.SwitchState<LoadTestState>();
        }

        public readonly StateMachine<BootstrapTest> StateMachine;
    }
}
