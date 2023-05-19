using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Data.StaticData.GunData.GravityGunData;
using Data.StaticData.GunData.MountRemoveData;
using Data.StaticData.GunData.PortalGunData;
using Data.StaticData.GunData.ScaleGunData;
using Infrastructure;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Services.Factories.PortalFactory;
using Services.Input;
using Unit.GravityGun;
using Unit.Mount;
using Unit.MountRemote;
using Unit.Portal;
using Unit.ScaleGun;
using Unit.UniversalGun;
using UnityEngine;

namespace Services.Factories.GunsFactory
{
    public class GunFactory : IGunFactory
    {
        public GunFactory(ICoroutineRunner coroutineRunner,
            IPortalFactory portalFactory,
            IAbstractFactory abstractFactory, 
            PlayerInputActionReader playerInputActionReader,
            GravityGunData gravityGunData,
            ScaleGunData scaleGunData,
            PortalGunData portalGunData,
            MountRemoveData mountRemoveData,
            ICameraContainer cameraContainer)
        {
            _abstractFactory = abstractFactory;
            _playerInputActionReader = playerInputActionReader;
            _gravityGunData = gravityGunData;
            _scaleGunData = scaleGunData;
            _portalGunData = portalGunData;
            _mountRemoveData = mountRemoveData;
            _cameraContainer = cameraContainer;
            _portalFactory = portalFactory;
            _coroutineRunner = coroutineRunner;
        }

        public void Construct(Transform playerPickPlaceInHand)
        {
            _positionInHand = playerPickPlaceInHand;
        }

        private readonly IAbstractFactory _abstractFactory;

        private readonly PlayerInputActionReader _playerInputActionReader;

        private readonly GravityGunData _gravityGunData;

        private readonly ScaleGunData _scaleGunData;

        private readonly PortalGunData _portalGunData;

        private readonly MountRemoveData _mountRemoveData;

        private readonly ICameraContainer _cameraContainer;

        private readonly IPortalFactory _portalFactory;

        private readonly ICoroutineRunner _coroutineRunner;

        private UniversalGunView _universalGunView;

        private Transform _positionInHand;

        public PortalGun CreatePortalGun()
        {
            return new PortalGun(_portalFactory, 
                _playerInputActionReader, _cameraContainer, _portalGunData, _universalGunView);
        }

        public GravityGun CreateGravityGun()
        {
            return new GravityGun(_coroutineRunner,
                _playerInputActionReader, _gravityGunData, _cameraContainer, _universalGunView);
        }

        public ScaleGun CreateScaleGun()
        {
            return new ScaleGun(_playerInputActionReader,
                _scaleGunData, _cameraContainer, _universalGunView);
        }

        public async Task<MountRemote> CreateMountRemove()
        {
            return new MountRemote(_playerInputActionReader, 
                _cameraContainer, _mountRemoveData, _universalGunView, 
                await CreateView<MountView>(AssetsAddressablesConstants.MOUNT_VIEW_PREFAB), 
                _positionInHand,
                _coroutineRunner);
        }

        public async Task<UniversalGunView> CreateUniversalGunView()
        {
            _universalGunView = await CreateView<UniversalGunView>(AssetsAddressablesConstants.UNIVERSAL_GUN_VIEW);

            var transform = _universalGunView.transform;

            transform.parent = _positionInHand;
            
            transform.position = _positionInHand.position;
            transform.rotation = _positionInHand.rotation;

            return _universalGunView;
        }

        private async Task<T> CreateView<T>(string viewPath)
        {
            var view =
                await _abstractFactory.CreateInstance<GameObject>(viewPath);

            return view.GetComponent<T>();
        } 
    }
}
