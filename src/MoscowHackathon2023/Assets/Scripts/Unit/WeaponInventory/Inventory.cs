using System.Collections.Generic;
using System.Threading.Tasks;
using Data.StaticData.GunData;
using Services.Containers;
using Services.Factories.UIFactory;
using Services.Input;
using Unit.UniversalGun;
using Unit.Weapon;
using UnityEngine;

namespace Unit.WeaponInventory
{
    public class Inventory
    {
        private List<IWeaponed> _weapons = new List<IWeaponed>();
        private InventoryView _inventoryView;
        private readonly IGameInstancesContainer _gameInstancesContainer;
        private IWeaponed _currentWeaponed;
        private int _currentIndexWeapon;

        public List<IWeaponed> Weapons => _weapons;

        public Inventory(PlayerInputActionReader playerInputActionReader,
            IGameInstancesContainer gameInstancesContainer,
            List<IWeaponed> weapons = null)
        {
            _gameInstancesContainer = gameInstancesContainer;

            if (weapons != null)
            {
                weapons.ForEach(weapon => SetupWeaponInInventory(weapon.GunData));
            }
            
            playerInputActionReader.IsMouseScroll += Switch;            
        }

        /*public async Task ShowPanel(Transform panel) 
        {            
            _inventoryView =  await _uiFactory.CreateInventoryPanel(panel);
        }

        public void HidePanel() 
        {
            _uiFactory.DestroyInventoryPanel();
        }*/

        public void AddWeaponToInventory(IWeaponed weapon) 
        {            
            _weapons.Add(weapon);
        }
        
        public void DisplayGunViewInInventory(IWeaponed weapon)
        {
            SetupWeaponInInventory(weapon.GunData);
        }

        private void Switch(float valueScroll) 
        {
            if (_weapons != null) 
            {
                if (valueScroll > 0) 
                {
                    SwitchForward();
                }
                else if(valueScroll < 0)
                {
                    SwitchBackward();
                }            
            }       
        }

        private void SwitchForward() 
        { 
            _currentIndexWeapon += 1;
            if (_currentIndexWeapon == _weapons.Count)
                _currentIndexWeapon = 0;

            ChangeWeapon(_weapons[_currentIndexWeapon]);
        }

        private void SwitchBackward() 
        { 
            _currentIndexWeapon -= 1;
            if (_currentIndexWeapon == -1)
                _currentIndexWeapon = _weapons.Count - 1;

            ChangeWeapon(_weapons[_currentIndexWeapon]);
        }

        private void ChangeWeapon(IWeaponed weapon) 
        {
            if (!_gameInstancesContainer.CollectedGunViews.Contains(weapon.GunData.GunType))
            {
                return;
            }
            
            if (_currentWeaponed != null) 
            {
                _currentWeaponed.Deselect();                
            }
            
            _currentWeaponed = weapon;
            _currentWeaponed.Select();
        }

        private void SetupWeaponInInventory(BaseGunData dataWeapon) 
        {            
            //_inventoryView.AddWeapon(dataWeapon.InventoryIcon, dataWeapon.IndexWeapon);
        }
    }
}
