using Cinemachine;
using Data.AssetsAddressablesConstants;
using Data.StaticData.PlayerData;
using Infrastructure.ProjectStateMachine.Base;
using PixelCrushers.DialogueSystem;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Services.Factories.GunsFactory;
using Services.Factories.PortalFactory;
using Services.Factories.UIFactory;
using Services.Input;
using Unit.CameraContainer;
using Unit.GravityGun;
using Unit.MountRemote;
using Unit.Player;
using Unit.Portal;
using Unit.ScaleGun;
using Unit.WeaponInventory;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameSetUpState : IState<Bootstrap>, IEnterable
    {
        public Bootstrap Initializer { get; }

        private readonly IGunFactory _gunFactory;
        private readonly IAbstractFactory _abstractFactory;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private readonly ICameraContainer _cameraContainer;
        private readonly PlayerBaseSettings _playerSettings;
        private readonly IUIFactory _uiFactory;
        private readonly IPlayerContainer _playerContainer;

        private Inventory _inventory;
        private PortalGun _portalGun;
        private GravityGun _gravityGun;
        private ScaleGun _scaleGun;
        private MountRemote _mountRemote;

        public GameSetUpState(Bootstrap initializer,IGunFactory gunFactory,
            IAbstractFactory abstractFactory,
            PlayerInputActionReader playerInputActionReader,
            ICameraContainer cameraContainer,
            PlayerBaseSettings playerSettings,
            IUIFactory uiFactory,
            IPlayerContainer playerContainer)
        {
            Initializer = initializer;
            _gunFactory = gunFactory;
            _abstractFactory = abstractFactory;
            _playerInputActionReader = playerInputActionReader;
            _cameraContainer = cameraContainer;
            _playerSettings = playerSettings;
            _uiFactory = uiFactory;
            _playerContainer = playerContainer;
        }

        public async void OnEnter()
        {
            var playerInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PLAYER_PREFAB);

            var cameraInstance = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.CAMERA_PREFAB);
            
            var demoNPC = await
                _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.DEMO_NPC);
            
            var cameraChildContainer = cameraInstance.GetComponentInChildren<CameraChildContainer>();

            SetUp(playerInstance, cameraInstance, cameraChildContainer.WeaponContainer, demoNPC);

            playerInstance.transform.position = new Vector3(-200, 1, -100);

            _portalGun = await _gunFactory.CreatePortalGun();
            _gravityGun = await _gunFactory.CreateGravityGun();
            _scaleGun = await _gunFactory.CreateScaleGun();
            _mountRemote = await _gunFactory.CreateMountRemove();

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
            GameObject demoNPC)
        {
            _playerContainer.SetUp(playerInstance);
            
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
            
            if (playerInstance.TryGetComponent(out PlayerPickUp playerPick))
            {
                playerPick.Construct(_cameraContainer);   
                
                _gunFactory.Construct(weaponContainer);
            }

            if (playerInstance.TryGetComponent(out PlayerMovement playerMovement))
            {
                playerMovement.Construct(_playerInputActionReader, mainCamera, _playerSettings);
            }
            
            if (playerInstance.TryGetComponent(out PlayerRotation playerRotation))
            {
                playerRotation.Construct(mainCamera);
            }

            if (demoNPC.TryGetComponent(out DialogueSystemTrigger dialogueSystemTrigger))
            {
                dialogueSystemTrigger.conversationActor = playerInstance.transform;
            }
        }
    }
}