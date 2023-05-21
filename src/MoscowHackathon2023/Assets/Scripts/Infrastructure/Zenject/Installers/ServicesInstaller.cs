using Data.StaticData.GunData;
using Data.StaticData.GunData.GravityGunData;
using Data.StaticData.GunData.MountRemoveData;
using Data.StaticData.GunData.PortalGunData;
using Data.StaticData.GunData.ScaleGunData;
using Data.StaticData.PlayerData;
using Services.AssetsAddressables;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Services.Factories.GunsFactory;
using Services.Factories.PortalFactory;
using Services.Factories.UIFactory;
using Services.Input;
using Services.PlaySounds;
using Services.StaticData;
using Unit.Portal;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Infrastructure.Zenject.Installers
{
    public class ServicesInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private PlayerInputActionReader _playerInputActionReader;
        [SerializeField] private GravityGunData _gravityGunData;
        [SerializeField] private ScaleGunData _scaleGunData;
        [SerializeField] private PortalGunData _portalGunData;
        [SerializeField] private MountRemoveData _mountRemoveData;
        [SerializeField] private PlayerBaseSettings _playerSettings;

        public override void InstallBindings()
        {
            BindUIFactory();
            BindGunFactory();
            BindPortalFactory();
            BindGameInstancesContainer();
            BindPlayerSettings();
            BindStaticDataService();
            BindAbstractFactory();
            BindCoroutineRunner();
            BindGunsStaticDataData();
            BindPlaySoundService();
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
        
        private void BindPlaySoundService()
        {
            Container.BindInterfacesTo<PlaySoundsService>().AsSingle();
        }

        private void BindPlayerInputActionsReader()
        {
            Container.Bind<PlayerInputActionReader>().FromInstance(_playerInputActionReader).AsSingle();
        }
        
        
        private void BindGameInstancesContainer()
        {
            Container.BindInterfacesTo<GameInstancesContainer>().AsSingle();
        }
        
        private void BindStaticDataService()
        {
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
        }
        
        private void BindGunsStaticDataData()
        {
            Container.Bind<GravityGunData>().FromInstance(_gravityGunData).AsSingle();
            Container.Bind<ScaleGunData>().FromInstance(_scaleGunData).AsSingle();
            Container.Bind<PortalGunData>().FromInstance(_portalGunData).AsSingle();
            Container.Bind<MountRemoveData>().FromInstance(_mountRemoveData).AsSingle();
        }

        private void BindPlayerSettings()
        {
            Container.Bind<PlayerBaseSettings>().FromInstance(_playerSettings).AsSingle();
        }
        
        private void BindGunFactory()
        {
            Container.BindInterfacesTo<GunFactory>().AsSingle();
        }

        private void BindPortalFactory()
        {
            Container.BindInterfacesTo<PortalFactory>().AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
        }
    }
}