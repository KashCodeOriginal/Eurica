using System.Collections.Generic;
using Services.Factories.UIFactory;
using UnityEngine;
using Zenject;

namespace Unit.WeaponInventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform _placeSlots;
        
        private Dictionary<int, SlotView> _slots = new Dictionary<int, SlotView>();
        private IUIFactory _uiFactory;
        
        public void Construct(IUIFactory uiFactory) 
        {
            _uiFactory = uiFactory;
        }

        public void ChangeCurrentActiveWeapon(int indexWeapon) 
        {
            foreach(var slot in _slots)
            {
                if (slot.Key == indexWeapon)
                {
                    slot.Value.Highlight();
                }
                else
                {
                    slot.Value.StopHighlighting();
                }
            }                       
        }

        public async void AddWeapon(Sprite spriteWeapon, int indexWeapon) 
        {
            var slotView = await _uiFactory.CreateSlot(_placeSlots, spriteWeapon);
            _slots.Add(indexWeapon, slotView);            
        }
    }
}
