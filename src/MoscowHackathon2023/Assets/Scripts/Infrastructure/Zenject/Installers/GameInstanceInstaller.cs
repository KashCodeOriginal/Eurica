using Infrastructure;
using Infrastructure.ProjectStateMachine.States;

namespace Zenject.Installers
{
    public class GameInstanceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootstrap();
            BindBootstrapState();
        }

        private void BindBootstrap()
        {
            Container.Bind<Bootstrap>().AsSingle().NonLazy();
        }

        private void BindBootstrapState()
        {
            Container.Bind<IInitializable>().To<BootstrapState>().AsSingle().NonLazy();
        }
    }
}