using Infrastructure.ProjectStateMachine.Base;
using Services.Factories.UIFactory;
using UI.MainMenuScreen;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class MainMenuState : IState<Bootstrap>, IEnterable, IExitable
    {
        public Bootstrap Initializer { get; }
        
        private readonly IUIFactory _uiFactory;

        private MainMenuScreenController _mainMenuScreenController;

        public MainMenuState(Bootstrap initializer, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Initializer = initializer;
        }

        public async void OnEnter()
        {
            var mainMenuScreen = await _uiFactory.CreateMainMenuScreen();

            if (!mainMenuScreen.TryGetComponent(out MainMenuScreenController mainMenuScreenController))
            {
                return;
            }
            
            _mainMenuScreenController = mainMenuScreenController;

            _mainMenuScreenController.OnPlayButtonClicked += SwitchStateToGameLoading;
            _mainMenuScreenController.OnTestButtonClicked += SwitchStateToTestScene;
        }

        private void SwitchStateToGameLoading()
        {
            Initializer.StateMachine.SwitchState<GameLoadingState>();
        }

        private void SwitchStateToTestScene()
        {
            Initializer.StateMachine.SwitchState<TestState>();
        }

        public void OnExit()
        {
            _mainMenuScreenController.OnPlayButtonClicked -= SwitchStateToGameLoading;
            
            _uiFactory.DestroyMainMenuScreen();
        }
    }
}