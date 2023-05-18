using Services.AssetsAddressables;
using Services.Factories.UIFactory;
using Zenject;

namespace Infrastructure.Zenject.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAddressablesProvider();
            BindUIFactory();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }
        
        private void BindAddressablesProvider()
        {
            Container.BindInterfacesTo<AssetsAddressablesProvider>().AsSingle();
        }
    }
}