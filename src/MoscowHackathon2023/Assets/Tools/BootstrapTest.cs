using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories;
using Services.Factories.AbstractFactory;
using Services.Input;

namespace Tools
{
    public class BootstrapTest
    {
        public readonly StateMachine<BootstrapTest> StateMachine;

        public BootstrapTest(GunFactory gunFactory,
            IAbstractFactory abstractFactory, 
            PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer)
        {
            StateMachine = new StateMachine<BootstrapTest>(                
                new LoadTestState(gunFactory, abstractFactory, playerInputActionReader, cameraContainer));

                StateMachine.SwitchState<LoadTestState>();
        }
    }
}
