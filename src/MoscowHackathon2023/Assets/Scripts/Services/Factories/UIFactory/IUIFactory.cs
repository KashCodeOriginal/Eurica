using System.Threading.Tasks;
using Unit.WeaponInventory;
using UnityEngine;

namespace Services.Factories.UIFactory
{
    public interface IUIFactory : IUIInfo
    {
        public void CreateMenuLoadingScreen();
        public void DestroyMenuLoadingScreen();
        
        public Task<GameObject> CreateMainMenuScreen();
        public void DestroyMainMenuScreen();
        
        public void CreateGameLoadingScreen();
        public void DestroyGameLoadingScreen();
        
        public Task<GameObject> CreateGameplayScreen();
        public void DestroyGameplayScreen();
        
        public Task<InventoryView> CreateInventoryPanel(Transform placemarker);        
        public void DestroyInventoryPanel();

        public Task<SlotView> CreateSlot(Transform placeMarker, Sprite spriteWeapon);
        public void DestroySlot();
    }
}