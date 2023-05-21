using Cinemachine;
using Services.Factories.UIFactory;
using UI.GameplayScreen;
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

        public void TurnOnPlayer()
        {
            Player.SetActive(true);
            _uiFactory.GameplayScreen.GetComponent<GameplayScreen>().InventoryTransform.gameObject.SetActive(true);
            _uiFactory.GameplayScreen.GetComponent<GameplayScreen>().StaticCanvas.SetActive(true);
            _uiFactory.GameplayScreen.SetActive(true);
        }

        public void TurnOffPlayer()
        {
            Player.SetActive(false);
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