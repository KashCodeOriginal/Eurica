using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Data.StaticData.GunData.GravityGunData;
using Data.StaticData.GunData.ScaleGunData;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Services.Input;
using Tools;
using Unit.GravityGun;
using Unit.Mount;
using Unit.MountRemote;
using Unit.Portal;
using Unit.ScaleGun;
using UnityEngine;

namespace Services.Factories
{
    public class GunFactory
    {
        private readonly IAbstractFactory _abstractFactory;
        private readonly PositionPlacemarkerTestScene _positionPlacemarkerTestScene;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly GravityGunData _gravityGunData;
        private readonly ScaleGunData _scaleGunData;
        private readonly ICameraContainer _cameraContainer;
        private readonly PortalFactory _portalFactory;
        private readonly ICoroutineRunner _coroutinerRunner;

        public GunFactory(ICoroutineRunner coroutinerRunner,
            PortalFactory portalFactory,
            PositionPlacemarkerTestScene positionPlacemarkerTestScene, 
            IAbstractFactory abstractFactory, 
            PlayerInputActionReader playerInputActionReader,
            GravityGunData gravityGunData,
            ScaleGunData scaleGunData,
            ICameraContainer cameraContainer)
        {
            _abstractFactory = abstractFactory;
            _positionPlacemarkerTestScene = positionPlacemarkerTestScene;
            _playerInputActionReader = playerInputActionReader;
            _gravityGunData = gravityGunData;
            _scaleGunData = scaleGunData;
            _cameraContainer = cameraContainer;
            _portalFactory = portalFactory;
            _coroutinerRunner = coroutinerRunner;
        }

        public async Task<PortalGun> CreatePortalGun()
        {
            return new PortalGun(_portalFactory, 
                await CreateView<PortalGunView>(AssetsAddressablesConstants.PORTAL_GUN_VIEW_PREFAB,
                    _positionPlacemarkerTestScene.PlaceSpawnPortalGun),
                _playerInputActionReader, _cameraContainer);
        }

        public async Task<GravityGun> CreateGravityGun()
        {
            return new GravityGun(_coroutinerRunner, 
                await CreateView<GravityGunView>(AssetsAddressablesConstants.GRAVITY_GUN_VIEW_PREFAB,
                _positionPlacemarkerTestScene.PlaceSpawnGravityGun),
                _playerInputActionReader, _gravityGunData, _cameraContainer);
        }
        
        public async Task<ScaleGun> CreateScaleGun()
        {
            return new ScaleGun(_playerInputActionReader,
                await CreateView<ScaleGunView>(AssetsAddressablesConstants.SCALE_GUN_VIEW_PREFAB,
                _positionPlacemarkerTestScene.PlaceSpawnScaleGun),
                _scaleGunData, _cameraContainer);
        }

        public async Task<MountRemote> CreateMountRemove()
        {
            return new MountRemote(_playerInputActionReader, 
                await CreateView<MountRemoteView>(AssetsAddressablesConstants.MOUNT_REMOTE_VIEW,
                _positionPlacemarkerTestScene.PlaceSpawnMountRemove),
                await CreateView<MountView>(AssetsAddressablesConstants.MOUNT_VIEW_PREFAB,
                    _positionPlacemarkerTestScene.PlaceSpawnMountRemove), _cameraContainer);
        }

        private async Task<T> CreateView<T>(string viewPath, Transform viewTransform)
        {
            var view =
                await _abstractFactory.CreateInstance<GameObject>(viewPath);

            view.transform.position = viewTransform.position;
            view.transform.rotation = viewTransform.rotation;        
            
            return view.GetComponent<T>();
        } 
    }
}
