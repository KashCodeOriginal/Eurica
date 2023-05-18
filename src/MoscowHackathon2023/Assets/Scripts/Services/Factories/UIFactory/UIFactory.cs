using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Services.AssetsAddressables;
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
    }
}