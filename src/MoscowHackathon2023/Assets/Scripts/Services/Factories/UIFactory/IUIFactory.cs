using System.Threading.Tasks;
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
    }
}