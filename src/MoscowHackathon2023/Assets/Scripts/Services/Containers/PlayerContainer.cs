using Services.Factories.UIFactory;
using UnityEngine;

namespace Services.Containers
{
    public class PlayerContainer : IPlayerContainer
    {
        public GameObject Player { get; private set; }

        private IUIFactory _uiFactory;
        private Transform _weaponContainer;

        public void SetUp(GameObject player, IUIFactory uiFactory, Transform weaponContainer)
        {
            Player = player;
            _uiFactory = uiFactory;
            _weaponContainer = weaponContainer;
        }

        public void TurnOnPlayer()
        {
            Player.SetActive(false);
            _weaponContainer.gameObject.SetActive(false);
            _uiFactory.GameplayScreen.SetActive(false);
        }

        public void TurnOffPlayer()
        {
            Player.SetActive(true);
            _weaponContainer.gameObject.SetActive(true);
            _uiFactory.GameplayScreen.SetActive(true);
        }
    }
}