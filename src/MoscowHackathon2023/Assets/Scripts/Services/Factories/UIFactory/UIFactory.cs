using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Services.AssetsAddressables;
using Unit.WeaponInventory;
using UnityEngine;
using Zenject;

namespace Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        public UIFactory(DiContainer container, IAssetsAddressablesProvider assetsAddressableService)
        {
            _container = container;
            _assetsAddressableService = assetsAddressableService;
        }
        
        private readonly DiContainer _container;
        private readonly IAssetsAddressablesProvider _assetsAddressableService;

        public GameObject MenuLoadingScreen { get; private set; }
        public GameObject MainMenuScreen { get; private set; }
        public GameObject GameLoadingScreen { get; private set; }
        public GameObject GameplayScreen { get; private set; }
        
        public GameObject InventoryPanel { get; private set; }
        
        //public GameObject Slot { get; private set; }

        public async void CreateMenuLoadingScreen()
        {
            var menuLoadingScreenPrefab =
                await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.MENU_LOADING_SCREEN);

            MenuLoadingScreen = _container.InstantiatePrefab(menuLoadingScreenPrefab);
        }

        public void DestroyMenuLoadingScreen()
        {
            Object.Destroy(MenuLoadingScreen);
        }

        public async Task<GameObject> CreateMainMenuScreen()
        {
            var mainMenuPrefab =
                await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.MAIN_MENU_SCREEN);

            MainMenuScreen = _container.InstantiatePrefab(mainMenuPrefab);

            return MainMenuScreen;
        }

        public void DestroyMainMenuScreen()
        {
            Object.Destroy(MainMenuScreen);
        }

        public async void CreateGameLoadingScreen()
        {
            var gameLoadingScreenPrefab =
                await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.GAME_LOADING_SCREEN);

            GameLoadingScreen = _container.InstantiatePrefab(gameLoadingScreenPrefab);
        }

        public void DestroyGameLoadingScreen()
        {
            Object.Destroy(GameLoadingScreen);
        }

        public async Task<GameObject> CreateGameplayScreen()
        {
            var gameplayScreenPrefab =
                await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.GAMEPLAY_SCREEN);

            GameplayScreen = _container.InstantiatePrefab(gameplayScreenPrefab);

            return GameplayScreen;
        }

        public void DestroyGameplayScreen()
        {
            Object.Destroy(GameplayScreen);
        }
        
        /*public async Task<InventoryView> CreateInventoryPanel(Transform placeMarker)
        {
            var inventoryPanelPrefabs =
                await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.INVENTORY_PANEL);

            InventoryPanel = _container.InstantiatePrefab(inventoryPanelPrefabs, placeMarker);

            if (!InventoryPanel.TryGetComponent(out InventoryView inventoryView))
            {
                return null;
            }
            
            inventoryView.Construct(this);
                    
            return inventoryView;

        }

        public void DestroyInventoryPanel()
        {
            Object.Destroy(InventoryPanel);
        }*/
        
        /*public async Task<SlotView> CreateSlot(Transform placeMarker, Sprite spriteWeapon)
        {
            var slotPrefab =
                await _assetsAddressableService.GetAsset<GameObject>(AssetsAddressablesConstants.SLOT);

            Slot = _container.InstantiatePrefab(slotPrefab, placeMarker);
            
            var slotView = Slot.GetComponent<SlotView>();
            
            slotView.SetIcon(spriteWeapon);            
            
            return slotView;
        }*/

        /*public void DestroySlot()
        {
            throw new System.NotImplementedException();
        }*/
    }
}