using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using Services.Factories;
using Unit.Portal;
using UnityEngine;
using Zenject;

namespace Tools
{
    public class TestInstanceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private PositionPlacemarkerTestScene  _positionPlacemarkerTestScene;
        
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.Bind<PositionPlacemarkerTestScene>().FromInstance(_positionPlacemarkerTestScene).AsSingle();
            BindBootstrap();
            BindBootstrapState();
            BindGunFactory();
            BindPortalFactory();
        }

        private void BindGunFactory() 
        { 
            Container.Bind<GunFactory>().AsSingle().NonLazy();
        }

        private void BindPortalFactory() 
        { 
            Container.Bind<PortalFactory>().AsSingle().NonLazy();
        }

        private void BindBootstrap()
        {
            Container.Bind<BootstrapTest>().AsSingle().NonLazy();            
        }

        private void BindBootstrapState()
        {
            Container.Bind<IInitializable>().To<BootstrapTestState>().AsSingle().NonLazy();
        }
    }
}