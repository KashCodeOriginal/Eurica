using System.Collections.Generic;
using System.Threading.Tasks;
using Data.StaticData.GunData;
using Services.Factories.UIFactory;
using Services.Input;
using Unit.Weapon;
using UnityEngine;

namespace Unit.WeaponInventory
{
    public class Inventory
    {
        private List<IWeaponed> _weapons = new List<IWeaponed>();
        private InventoryView _inventoryView;
        private IUIFactory _uiFactory;
        private Transform _placeMarker;
        private IWeaponed _currentWeaponed;
        private int _currentIndexWeapon;

        public Inventory(IUIFactory uiFactory, 
            PlayerInputActionReader playerInputActionReader, 
            Transform placeMarker, 
            List<IWeaponed> weapons = null)
        {
            _uiFactory = uiFactory;
            _placeMarker = placeMarker;

            if (weapons != null)
            {
                _weapons.ForEach(weapon => SetupWeaponInInventory(weapon.GunData));
            }
            
            playerInputActionReader.IsMouseScroll += Switch;            
        }

        public async Task ShowPanel() 
        {            
            _inventoryView =  await _uiFactory.CreateInventoryPanel(_placeMarker);
            Debug.Log(_inventoryView);
        }

        public void HidePanel() 
        {
            _uiFactory.DestroyInventoryPanel();
        }

        public void CollectWeapon(IWeaponed weapon) 
        {            
            _weapons.Add(weapon);
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
            if (_currentWeaponed != null) 
            {
                _currentWeaponed.Release();                
            }
            
            _currentWeaponed = weapon;
            _currentWeaponed.PickUp();
            
            _inventoryView.ChangeCurrentActiveWeapon(_currentWeaponed.GunData.IndexWeapon);
        }

        private void SetupWeaponInInventory(BaseGunData dataWeapon) 
        {            
            _inventoryView.AddWeapon(dataWeapon.InventoryIcon, dataWeapon.IndexWeapon);
        }
    }
}
