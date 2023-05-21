using Cinemachine;
using Services.Factories.UIFactory;
using UnityEngine;

namespace Services.Containers
{
    public interface IGameInstancesContainer
    {
        public GameObject Player { get; }
        public Camera Camera { get; }
        public CinemachineBrain CinemachineBrain { get; }

        public void SetUpPlayer(GameObject player, IUIFactory uiFactory, Transform weaponContainer);
        public void SetUpCamera(Camera camera, CinemachineBrain cinemachineBrain);
        public void TurnOnPlayer();
        public void TurnOffPlayer();  
        public void TurnOnWeapon();
        public void TurnOffWeapon();
    }
}