using System;
using Services.Containers;
using Unit.Base;
using Unit.UniversalGun;
using UnityEngine;
using Zenject;

namespace Unit.Table
{
    public class TableOfIdeas : MonoBehaviour, IInteractable
    {
        [Inject]
        public void Construct(IGameInstancesContainer gameInstancesContainer)
        {
            _gameInstancesContainer = gameInstancesContainer;
        }
        
        [SerializeField] private GunTypes _gunType;

        private IGameInstancesContainer _gameInstancesContainer;

        public void Interact()
        {
            var universalGunView = _gameInstancesContainer.UniversalGunView;
            
            switch (_gunType)
            {
                case GunTypes.Portal:
                    _gameInstancesContainer.Inventory.Weapons[0].SetUpUniversalView(universalGunView);
                    break;
                case GunTypes.Gravity:
                    _gameInstancesContainer.Inventory.Weapons[1].SetUpUniversalView(universalGunView);
                    break;
                case GunTypes.Scale:
                    _gameInstancesContainer.Inventory.Weapons[2].SetUpUniversalView(universalGunView);
                    break;
            }
        }
    }
}
