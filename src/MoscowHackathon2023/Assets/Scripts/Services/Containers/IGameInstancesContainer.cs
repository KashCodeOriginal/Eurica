using System.Collections.Generic;
using Cinemachine;
using Services.Factories.UIFactory;
using Unit.UniversalGun;
using Unit.WeaponInventory;
using UnityEngine;

namespace Services.Containers
{
    public interface IGameInstancesContainer
    {
        public GameObject Player { get; }
        public Camera Camera { get; }
        public CinemachineBrain CinemachineBrain { get; }
        public Inventory Inventory { get;}
        public UniversalGunView UniversalGunView { get; }
        public List<GunTypes> CollectedGunViews { get; }

        public void SetUpPlayer(GameObject player, IUIFactory uiFactory, Transform weaponContainer);
        public void SetUpCamera(Camera camera, CinemachineBrain cinemachineBrain);
        public void SetUpInventory(Inventory inventory);
        public void SetUpUniversalGunView(UniversalGunView universalGunView);
        public void TurnOnPlayer();
        public void TurnOffPlayer();  
        public void TurnOnPlayerUI();
        public void TurnOffPlayerIU();  
        public void TurnOnWeapon();
        public void TurnOffWeapon();
        public void AddViewGun(GunTypes gunType);
    }
}