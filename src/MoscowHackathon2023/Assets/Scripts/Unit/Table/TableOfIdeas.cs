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

        public event Action IsPortalGunCreated;
        public event Action IsGravityGunCreated;
        public event Action IsScaleGunCreated;

        public void Interact()
        {
            var universalGunView = _gameInstancesContainer.UniversalGunView;
            
            switch (_gunType)
            {
                case GunTypes.Portal:
                    _gameInstancesContainer.Inventory.Weapons[0].SetUpUniversalView(universalGunView);
                    _gameInstancesContainer.AddViewGun(GunTypes.Portal);
                    
                    universalGunView.PortalGunBody.SetActive(true);

                    break;
                case GunTypes.Gravity:
                    _gameInstancesContainer.Inventory.Weapons[1].SetUpUniversalView(universalGunView);
                    _gameInstancesContainer.AddViewGun(GunTypes.Gravity);
                    
                    _gameInstancesContainer.TurnOnWeapon();
                    
                    IsGravityGunCreated?.Invoke();
                    
                    _gameInstancesContainer.TurnOnWeapon();
                    
                    break;
                case GunTypes.Scale:
                    _gameInstancesContainer.Inventory.Weapons[2].SetUpUniversalView(universalGunView);
                    
                    _gameInstancesContainer.AddViewGun(GunTypes.Scale);
                    
                    universalGunView.ScaleGunBody.SetActive(true);
                    
                    IsScaleGunCreated?.Invoke();
                    
                    break;
                case GunTypes.None:
                    break;
            }
        }

        public void SetUpType(GunTypes gunType)
        {
            _gunType = gunType;
        }
    }
}
