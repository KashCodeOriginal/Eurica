using Cinemachine;
using Data.AssetsAddressablesConstants;
using Infrastructure;
using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories;
using Services.Factories.AbstractFactory;
using Services.Input;
using Tools.FirstPersonCharacter.Scripts;
using Unit.GravityGun;
using Unit.MountRemote;
using Unit.Player;
using Unit.Portal;
using Unit.ScaleGun;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Tools
{
    public class LoadTestState : IState<BootstrapTest>, IEnterable, IExitable
    {
        public BootstrapTest Initializer { get; }

        private readonly GunFactory _gunFactory;
        private readonly IAbstractFactory _abstractFactory;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly ICameraContainer _cameraContainer;

        private PortalGun _portalGun;
        private GravityGun _gravityGun;
        private ScaleGun _scaleGun;
        private MountRemote _mountRemote;

        public LoadTestState(GunFactory gunFactory,
            IAbstractFactory abstractFactory,
            PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer)
        {
            _gunFactory = gunFactory;
            _abstractFactory = abstractFactory;
            _playerInputActionReader = playerInputActionReader;
            _cameraContainer = cameraContainer;
        }

        public async void OnEnter()
        {
            _portalGun = await _gunFactory.CreatePortalGun();
            _gravityGun = await _gunFactory.CreateGravityGun();
            _scaleGun = await _gunFactory.CreateScaleGun();
            _mountRemote = await _gunFactory.CreateMountRemove();

            var playerInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PLAYER_PREFAB);

            var cameraInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.CAMERA_PREFAB);

            SetUp(playerInstance, cameraInstance);
        }

        private void SetUp(GameObject playerInstance, GameObject cameraInstance)
        {
            var virtualCamera = cameraInstance.GetComponentInChildren<CinemachineVirtualCamera>();
            
            var mainCamera = cameraInstance.GetComponentInChildren<Camera>();

            if (playerInstance.TryGetComponent(out PlayerContainer playerContainer))
            {
                var headTransform = playerContainer.HeadTransform;

                virtualCamera.Follow = headTransform;
            }

            _cameraContainer.SetUpCamera(cameraInstance.GetComponentInChildren<Camera>());


            if (playerInstance.TryGetComponent(out PlayerInteraction playerInteraction))
            {
                playerInteraction.Construct(_playerInputActionReader, _cameraContainer);   
            }
            
            if (playerInstance.TryGetComponent(out PlayerPickUp playerPick))
            {
                playerPick.Construct(_cameraContainer);   
            }
            
            if (playerInstance.TryGetComponent(out RigidbodyFirstPersonController rigidbodyFirstPersonController))
            {
                rigidbodyFirstPersonController.cam = mainCamera;
            }


            if (playerInstance.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.Construct(_playerInputActionReader, mainCamera.transform);
            }

            playerInstance.transform.position = new Vector3(0, 1f, 0);
        }

        public void OnExit()
        {
            
        }
    }
}