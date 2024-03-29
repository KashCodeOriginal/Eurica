using System;
using System.Threading.Tasks;
using Cinemachine;
using Data.AssetsAddressablesConstants;
using Data.StaticData.LevelData;
using Data.StaticData.PlayerData;
using Infrastructure.ProjectStateMachine.Base;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Services.Factories.GunsFactory;
using Services.Factories.UIFactory;
using Services.GameProgress;
using Services.Input;
using Services.PlaySounds;
using Services.StaticData;
using Unit.CameraContainer;
using Unit.GravityGun;
using Unit.MountRemote;
using Unit.Player;
using Unit.Portal;
using Unit.ScaleGun;
using Unit.UniversalGun;
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
        private readonly PlayerBaseSettings _playerSettings;
        private readonly IGameInstancesContainer _gameInstancesContainer;
        private readonly IStaticDataService _staticDataService;
        private readonly IPlaySoundsService _playSoundsService;
        private readonly IUIFactory _uiFactory;
        private readonly IGameProgressService _gameProgressService;

        private PortalGun _portalGun;
        private GravityGun _gravityGun;
        private ScaleGun _scaleGun;
        private MountRemote _mountRemote;

        private GameObject _universalGunView;

        public GameSetUpState(Bootstrap initializer,IGunFactory gunFactory,
            IAbstractFactory abstractFactory,
            PlayerInputActionReader playerInputActionReader,
            PlayerBaseSettings playerSettings,
            IGameInstancesContainer gameInstancesContainer,
            IStaticDataService staticDataService,
            IPlaySoundsService playSoundsService,
            IUIFactory uiFactory,
            IGameProgressService gameProgressService)
        {
            Initializer = initializer;
            _gunFactory = gunFactory;
            _abstractFactory = abstractFactory;
            _playerInputActionReader = playerInputActionReader;
            _playerSettings = playerSettings;
            _gameInstancesContainer = gameInstancesContainer;
            _staticDataService = staticDataService;
            _playSoundsService = playSoundsService;
            _uiFactory = uiFactory;
            _gameProgressService = gameProgressService;
        }

        public async void OnEnter(string arg)
        {
            var levelData = _staticDataService.ForLevel(arg);

            if (levelData.IsPlayerOnScene)
            {
                await CreatePlayerAndOtherEnvironment(levelData);
            }

            Initializer.StateMachine.SwitchState<GameplayState>();
        }

        private async Task CreatePlayerAndOtherEnvironment(LevelData levelData)
        {
            var playerInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PLAYER_PREFAB);

            var cameraInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.CAMERA_PREFAB);

            var audioSourceInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.AUDIO_SOURCE_PREFAB);

            var cameraChildContainer = cameraInstance.GetComponentInChildren<CameraChildContainer>();

            SetUp(playerInstance, cameraInstance, cameraChildContainer.WeaponContainer, audioSourceInstance);

            playerInstance.transform.position = levelData.PlayerSpawnPoint;
            playerInstance.transform.rotation = levelData.PlayerSpawnRotation;

            if (!levelData.IsPlayerInstancingAtStart)
            {
                playerInstance.SetActive(false);
                cameraChildContainer.WeaponContainer.gameObject.SetActive(false);
            }

            if (!levelData.IsPlayerCameraInstancingAtStart)
            {
                cameraInstance.SetActive(false);
            }

            var currentWeaponType = _gameProgressService.CurrentWeaponOnPlayer;

            if (currentWeaponType == GunTypes.None)
            {
                cameraChildContainer.WeaponContainer.gameObject.SetActive(false);
            }
            
            
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void SetUp(GameObject playerInstance, GameObject cameraInstance, Transform weaponContainer,
            GameObject audioSource)
        {
            _playSoundsService.SetUp(audioSource.GetComponent<AudioSource>());
            
            _gameInstancesContainer.SetUpPlayer(playerInstance, _uiFactory, weaponContainer);
            
            var mainCamera = cameraInstance.GetComponentInChildren<Camera>();

            _gameInstancesContainer.SetUpCamera(cameraInstance.GetComponentInChildren<Camera>(), 
                cameraInstance.GetComponentInChildren<CinemachineBrain>());
            
            if (playerInstance.TryGetComponent(out PlayerInteraction playerInteraction))
            {
                playerInteraction.Construct(_playerInputActionReader, _gameInstancesContainer);   
            }
            
            _gunFactory.Construct(weaponContainer);

            if (playerInstance.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.Construct(_playerInputActionReader, mainCamera, _playerSettings, _playSoundsService);
            }
        }
    }
}