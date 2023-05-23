using Infrastructure.ProjectStateMachine.Base;
using Services.Factories.UIFactory;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameLoadingState : IState<Bootstrap>, IEnterableWithOneArg<string>
    {
        private readonly IUIFactory _uiFactory;
        public Bootstrap Initializer { get; }

        public GameLoadingState(Bootstrap initializer, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            Initializer = initializer;
        }

        public async void OnEnter(string arg)
        {
            var asyncOperationHandler = Addressables.LoadSceneAsync(arg);

            _uiFactory.CreateGameLoadingScreen();

            await asyncOperationHandler.Task;
            
            Initializer.StateMachine.SwitchState<GameSetUpState, string>(arg);
        }
    }

}