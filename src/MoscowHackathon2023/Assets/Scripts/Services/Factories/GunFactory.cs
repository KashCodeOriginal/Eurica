using Data.AssetsAddressablesConstants;
using PortalMechanics;
using Services.Factories.AbstractFactory;
using Services.Input;
using System.Threading.Tasks;
using Unit.GravityGunMechanics;
using Unit.Portal;
using UnityEngine;
using Zenject.Installers;

namespace Unit
{
    public class GunFactory
    {
        private IAbstractFactory _abstractFactory;
        private PositionPlacemarkerTestScene _positionPlacemarkerTestScene;
        private PlayerInputActionReader _playerInputActionReader;
        private PortalFactory _portalFactory;
        private ICoroutineRunner _coroutinerRunner;

        public GunFactory(ICoroutineRunner coroutinerRunner, PortalFactory portalFactory, PositionPlacemarkerTestScene positionPlacemarkerTestScene, IAbstractFactory abstractFactory, PlayerInputActionReader playerInputActionReader)
        {
            _abstractFactory = abstractFactory;
            _positionPlacemarkerTestScene = positionPlacemarkerTestScene;
            _playerInputActionReader = playerInputActionReader;
            _portalFactory = portalFactory;
            _coroutinerRunner = coroutinerRunner;
        }

        public async Task<PortalGun> CreatePortalGun()
        {
            return new PortalGun(_portalFactory, await CreatePortalGunView(), _playerInputActionReader);
        }

        public async Task<GravityGun> CreateGravityGun()
        {
            return new GravityGun(_coroutinerRunner, await CreateGravityGunView(), _playerInputActionReader);
        }

        private async Task<PortalGunView> CreatePortalGunView()
        {
            var portalGun =
                await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PORTAL_GUN_VIEW_PREFAB);

            portalGun.transform.position = _positionPlacemarkerTestScene.PlaceSpawnPortalGun.transform.position;
            portalGun.transform.rotation = _positionPlacemarkerTestScene.PlaceSpawnPortalGun.transform.rotation;            
            return portalGun.GetComponent<PortalGunView>();
        }

        private async Task<GravityGunView> CreateGravityGunView()
        {
            var portalGun =
                await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.GRAVITY_GUN_VIEW_PREFAB);

            portalGun.transform.position = _positionPlacemarkerTestScene.PlaceSpawnGrawityGun.transform.position;
            portalGun.transform.rotation = _positionPlacemarkerTestScene.PlaceSpawnGrawityGun.transform.rotation;            
            return portalGun.GetComponent<GravityGunView>();
        }
    }
}
