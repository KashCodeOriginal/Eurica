using Services.AssetsAddressables;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure.Zenject.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputActionReader _playerInputActionReader;
        
        public override void InstallBindings()
        {
            BindUIFactory();
            BindAbstractFactory();
            BindAddressablesProvider();
            BindPlayerInputActionsReader();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }

        private void BindAbstractFactory()
        {
            Container.BindInterfacesTo<AbstractFactory>().AsSingle();
        }
        
        private void BindAddressablesProvider()
        {
            Container.BindInterfacesTo<AssetsAddressablesProvider>().AsSingle();
        }

        private void BindPlayerInputActionsReader()
        {
            Container.Bind<PlayerInputActionReader>().FromInstance(_playerInputActionReader);
        }
    }
}