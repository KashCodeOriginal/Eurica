using Cinemachine;
using Data.AssetsAddressablesConstants;
using Data.StaticData.PlayerData;
using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Services.Factories.GunsFactory;
using Services.Factories.UIFactory;
using Services.Input;
using Services.PlaySounds;
using Services.StaticData;
using Unit.CameraContainer;
using Unit.GravityGun;
using Unit.MountRemote;
using Unit.Player;
using Unit.Portal;
using Unit.ScaleGun;
using Unit.WeaponInventory;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameSetUpState : IState<Bootstrap>, IEnterableWithOneArg<string>
    {
        public Bootstrap Initializer { get; }

        private readonly IGunFactory _gunFactory;
        private readonly IAbstractFactory _abstractFactory;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly ICameraContainer _cameraContainer;
        private readonly PlayerBaseSettings _playerSettings;
        private readonly IUIFactory _uiFactory;
        private readonly IPlayerContainer _playerContainer;
        private readonly IStaticDataService _staticDataService;
        private readonly IPlaySoundsService _playSoundsService;

        private Inventory _inventory;
        private PortalGun _portalGun;
        private GravityGun _gravityGun;
        private ScaleGun _scaleGun;
        private MountRemote _mountRemote;
        
        private GameObject _universalGunView;

        public GameSetUpState(Bootstrap initializer,IGunFactory gunFactory,
            IAbstractFactory abstractFactory,
            PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer,
            PlayerBaseSettings playerSettings,
            IUIFactory uiFactory,
            IPlayerContainer playerContainer,
            IStaticDataService staticDataService,
            IPlaySoundsService playSoundsService)
        {
            Initializer = initializer;
            _gunFactory = gunFactory;
            _abstractFactory = abstractFactory;
            _playerInputActionReader = playerInputActionReader;
            _cameraContainer = cameraContainer;
            _playerSettings = playerSettings;
            _uiFactory = uiFactory;
            _playerContainer = playerContainer;
            _staticDataService = staticDataService;
            _playSoundsService = playSoundsService;
        }

        public async void OnEnter(string arg)
        {
            var levelData = _staticDataService.ForLevel(arg);
            
            var playerInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PLAYER_PREFAB);

            var cameraInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.CAMERA_PREFAB);
            
            var audioSourceInstance = await 
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.AUDIO_SOURCE_PREFAB);

            var cameraChildContainer = cameraInstance.GetComponentInChildren<CameraChildContainer>();

            SetUp(playerInstance, cameraInstance, cameraChildContainer.WeaponContainer, audioSourceInstance);

            playerInstance.transform.position = levelData.PlayerSpawnPoint;

            await _gunFactory.CreateUniversalGunView();

            _portalGun = _gunFactory.CreatePortalGun();
            _gravityGun = _gunFactory.CreateGravityGun();
            _scaleGun = _gunFactory.CreateScaleGun();
            _mountRemote = _gunFactory.CreateMountRemove();

            _inventory = new Inventory(_uiFactory, _playerInputActionReader, cameraChildContainer.InventoryContainer);

            await _inventory.ShowPanel();

            _inventory.CollectWeapon(_portalGun);
            _inventory.CollectWeapon(_gravityGun);
            _inventory.CollectWeapon(_scaleGun);
            _inventory.CollectWeapon(_mountRemote);

            Cursor.lockState = CursorLockMode.Locked;

            Initializer.StateMachine.SwitchState<GameplayState>();
        }

        private void SetUp(GameObject playerInstance, GameObject cameraInstance, Transform weaponContainer,
            GameObject audioSource)
        {
            _playerContainer.SetUp(playerInstance);
            
            _playSoundsService.SetUp(audioSource.GetComponent<AudioSource>());
            
            var virtualCamera = cameraInstance.GetComponentInChildren<CinemachineVirtualCamera>();
            
            var mainCamera = cameraInstance.GetComponentInChildren<Camera>();

            if (playerInstance.TryGetComponent(out PlayerChildContainer playerContainer))
            {
                var headTransform = playerContainer.HeadTransform;

                virtualCamera.Follow = headTransform;
            }

            _cameraContainer.SetUpCamera(cameraInstance.GetComponentInChildren<Camera>(), 
                cameraInstance.GetComponentInChildren<CinemachineBrain>());
            
            if (playerInstance.TryGetComponent(out PlayerInteraction playerInteraction))
            {
                playerInteraction.Construct(_playerInputActionReader, _cameraContainer);   
            }
            
            _gunFactory.Construct(weaponContainer);

            if (playerInstance.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.Construct(_playerInputActionReader, mainCamera, _playerSettings);
            }
            
            if (playerInstance.TryGetComponent(out PlayerRotation playerRotation))
            {
                playerRotation.Construct(mainCamera);
            }

        }
    }
}