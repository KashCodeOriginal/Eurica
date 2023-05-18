using Data.StaticData.PlayerData;
using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories;
using Services.Factories.AbstractFactory;
using Services.Factories.GunsFactory;
using Services.Factories.UIFactory;
using Services.Input;
using Unit.WeaponInventory;
using UnityEditor;

namespace Tools
{
    public class BootstrapTest
    {
        public readonly StateMachine<BootstrapTest> StateMachine;

        public BootstrapTest(IGunFactory gunFactory,
            IAbstractFactory abstractFactory, 
            PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer,
            PlayerBaseSettings playerSettings,
            IUIFactory uiFactory)
        {
            StateMachine = new StateMachine<BootstrapTest>(                
                new LoadTestState(gunFactory, 
                    abstractFactory,
                    playerInputActionReader, 
                    cameraContainer, 
                    playerSettings,
                    uiFactory));

                StateMachine.SwitchState<LoadTestState>();
        }
    }
}
