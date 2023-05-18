using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Services.Factories.AbstractFactory;
using Services.Input;
using Tools;
using Unit.GravityGunMechanics;
using Unit.Portal;
using Unit.ScaleGun;
using UnityEngine;
using Zenject.Installers;

namespace Services.Factories
{
    public class GunFactory
    {
        private IAbstractFactory _abstractFactory;
        private PositionPlacemarkerTestScene _positionPlacemarkerTestScene;
        private PlayerInputActionReader _playerInputActionReader;
        private PortalFactory _portalFactory;
        private ICoroutineRunner _coroutinerRunner;

        public GunFactory(ICoroutineRunner coroutinerRunner,
            PortalFactory portalFactory,
            PositionPlacemarkerTestScene positionPlacemarkerTestScene, 
            IAbstractFactory abstractFactory, 
            PlayerInputActionReader playerInputActionReader)
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
        
        public async Task<ScaleGun> CreateScaleGun()
        {
            return new ScaleGun(_playerInputActionReader, await CreateScaleGunView());
        }

        private async Task<PortalGunView> CreatePortalGunView()
        {
            var portalGun =
                await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PORTAL_GUN_VIEW_PREFAB);

            var transform = _positionPlacemarkerTestScene.PlaceSpawnPortalGun.transform;
            
            portalGun.transform.position = transform.position;
            portalGun.transform.rotation = transform.rotation;            
            return portalGun.GetComponent<PortalGunView>();
        }

        private async Task<GravityGunView> CreateGravityGunView()
        {
            var gravityGun =
                await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.GRAVITY_GUN_VIEW_PREFAB);

            var transform = _positionPlacemarkerTestScene.PlaceSpawnGravityGun.transform;
            
            gravityGun.transform.position = transform.position;
            gravityGun.transform.rotation = transform.rotation;            
            return gravityGun.GetComponent<GravityGunView>();
        }
        
        private async Task<ScaleGunView> CreateScaleGunView()
        {
            var scaleGun =
                await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.SCALE_GUN_VIEW_PREFAB);

            var transform = _positionPlacemarkerTestScene.PlaceSpawnScaleGun.transform;
            
            scaleGun.transform.position = transform.position;
            scaleGun.transform.rotation = transform.rotation;            
            return scaleGun.GetComponent<ScaleGunView>();
        }
    }
}
