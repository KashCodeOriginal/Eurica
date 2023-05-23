using Cinemachine;
using Services.Factories.UIFactory;
using UI.GameplayScreen;
using Unit.UniversalGun;
using Unit.WeaponInventory;
using UnityEngine;

namespace Services.Containers
{
    public class GameInstancesContainer : IGameInstancesContainer
    {
        public GameObject Player { get; private set; }
        private IUIFactory _uiFactory;
        private Transform _weaponContainer;
        public Camera Camera { get; private set; }
        public CinemachineBrain CinemachineBrain { get; private set; }
        public Inventory Inventory { get; private set; }
        public UniversalGunView UniversalGunView { get; private set; }

        public void SetUpPlayer(GameObject player, IUIFactory uiFactory, Transform weaponContainer)
        {
            Player = player;
            _uiFactory = uiFactory;
            _weaponContainer = weaponContainer;
        }
        
        public void SetUpCamera(Camera camera, CinemachineBrain cinemachineBrain)
        {
            Camera = camera;
            CinemachineBrain = cinemachineBrain;
        }

        public void SetUpInventory(Inventory inventory)
        {
            Inventory = inventory;
        }

        public void SetUpUniversalGunView(UniversalGunView universalGunView)
        {
            UniversalGunView = universalGunView;
        }

        public void TurnOnPlayer()
        {
            Player.SetActive(true);
        }

        public void TurnOffPlayer()
        {
            Player.SetActive(false);
            
        }
        public void TurnOnPlayerUI()
        {
            _uiFactory.GameplayScreen.GetComponent<GameplayScreen>().InventoryTransform.gameObject.SetActive(true);
            _uiFactory.GameplayScreen.GetComponent<GameplayScreen>().StaticCanvas.SetActive(true);
            _uiFactory.GameplayScreen.SetActive(true);
        }

        public void TurnOffPlayerIU()
        {
            _uiFactory.GameplayScreen.GetComponent<GameplayScreen>().InventoryTransform.gameObject.SetActive(false);
            _uiFactory.GameplayScreen.GetComponent<GameplayScreen>().StaticCanvas.SetActive(false);
            _uiFactory.GameplayScreen.SetActive(false);
        }

        public void TurnOnWeapon()
        {
            _weaponContainer.gameObject.SetActive(true);
        }

        public void TurnOffWeapon()
        {
            _weaponContainer.gameObject.SetActive(false);
        }
    }
}