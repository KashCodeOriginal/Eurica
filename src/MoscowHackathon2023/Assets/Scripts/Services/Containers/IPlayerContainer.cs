using Services.Factories.UIFactory;
using UnityEngine;

namespace Services.Containers
{
    public interface IPlayerContainer
    {
        public GameObject Player { get; }

        public void SetUp(GameObject player, IUIFactory uiFactory, Transform weaponContainer);
        public void TurnOnPlayer();
        public void TurnOffPlayer();
    }
}