using Data.AssetsAddressablesConstants;
using Infrastructure.ProjectStateMachine.Base;
using Services.Factories.UIFactory;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class MenuLoadingState : IState<Bootstrap>, IEnterable, IExitable
    {
        public Bootstrap Initializer { get; }
        
        private readonly IUIFactory _uiFactory;

        public MenuLoadingState(Bootstrap initializer, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Initializer = initializer;
        }

        public async void OnEnter()
        {
            _uiFactory.CreateMenuLoadingScreen();

            var asyncOperationHandler = Addressables.LoadSceneAsync(AssetsAddressablesConstants.MAIN_MENU_LEVEL);

            await asyncOperationHandler.Task;
            
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }

        public void OnExit()
        {
            _uiFactory.DestroyMenuLoadingScreen();
        }
    }
}