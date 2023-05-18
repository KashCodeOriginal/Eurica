using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Data.StaticData.GunData.GravityGunData;
using Data.StaticData.GunData.MountRemoveData;
using Data.StaticData.GunData.PortalGunData;
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
using Unit.WeaponInventory;
using UnityEngine;

namespace Services.Factories.GunsFactory
{
    public class GunFactory : IGunFactory
    {
        private readonly IAbstractFactory _abstractFactory;
        private readonly PositionPlacemarkerTestScene _positionPlaceMarkerTestScene;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly GravityGunData _gravityGunData;
        private readonly ScaleGunData _scaleGunData;
        private readonly PortalGunData _portalGunData;
        private readonly MountRemoveData _mountRemoveData;
        private readonly ICameraContainer _cameraContainer;
        private readonly PortalFactory _portalFactory;
        private readonly ICoroutineRunner _coroutineRunner;

        private Transform _positionInHand;

        public GunFactory(ICoroutineRunner coroutineRunner,
            PortalFactory portalFactory,
            PositionPlacemarkerTestScene positionPlaceMarkerTestScene, 
            IAbstractFactory abstractFactory, 
            PlayerInputActionReader playerInputActionReader,
            GravityGunData gravityGunData,
            ScaleGunData scaleGunData,
            PortalGunData portalGunData,
            MountRemoveData mountRemoveData,
            ICameraContainer cameraContainer)
        {
            _abstractFactory = abstractFactory;
            _positionPlaceMarkerTestScene = positionPlaceMarkerTestScene;
            _playerInputActionReader = playerInputActionReader;
            _gravityGunData = gravityGunData;
            _scaleGunData = scaleGunData;
            _portalGunData = portalGunData;
            _mountRemoveData = mountRemoveData;
            _cameraContainer = cameraContainer;
            _portalFactory = portalFactory;
            _coroutineRunner = coroutineRunner;
        }

        public async Task<PortalGun> CreatePortalGun()
        {
            return new PortalGun(_portalFactory, 
                await CreateView<PortalGunView>(AssetsAddressablesConstants.PORTAL_GUN_VIEW_PREFAB,
                    _positionPlaceMarkerTestScene.PlaceSpawnPortalGun),
                _playerInputActionReader, _cameraContainer, _portalGunData, _positionInHand);
        }

        public async Task<GravityGun> CreateGravityGun()
        {
            return new GravityGun(_coroutineRunner, 
                await CreateView<GravityGunView>(AssetsAddressablesConstants.GRAVITY_GUN_VIEW_PREFAB,
                    _positionPlaceMarkerTestScene.PlaceSpawnGravityGun),
                _playerInputActionReader, _gravityGunData, _cameraContainer, _positionInHand);
        }
        
        public async Task<ScaleGun> CreateScaleGun()
        {
            return new ScaleGun(_playerInputActionReader,
                await CreateView<ScaleGunView>(AssetsAddressablesConstants.SCALE_GUN_VIEW_PREFAB,
                    _positionPlaceMarkerTestScene.PlaceSpawnScaleGun),
                _scaleGunData, _cameraContainer, _positionInHand);
        }

        public async Task<MountRemote> CreateMountRemove()
        {
            return new MountRemote(_playerInputActionReader, 
                await CreateView<MountRemoteView>(AssetsAddressablesConstants.MOUNT_REMOTE_VIEW,
                    _positionPlaceMarkerTestScene.PlaceSpawnMountRemove),
                await CreateView<MountView>(AssetsAddressablesConstants.MOUNT_VIEW_PREFAB,
                    _positionPlaceMarkerTestScene.PlaceSpawnMountRemove), _cameraContainer, _mountRemoveData, _positionInHand);
        }

        public void Construct(Transform playerPickPlaceInHand)
        {
            _positionInHand = playerPickPlaceInHand;
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
