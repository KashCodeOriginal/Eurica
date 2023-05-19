using Infrastructure.ProjectStateMachine.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameLoadingState : IState<Bootstrap>, IEnterableWithOneArg<string>, IExitable
    {
        public Bootstrap Initializer { get; }

        public GameLoadingState(Bootstrap initializer)
        {
            Initializer = initializer;
        }

        public async void OnEnter(string arg)
        {
            var asyncOperationHandler = Addressables.LoadSceneAsync(arg);

            await asyncOperationHandler.Task;
            
            Initializer.StateMachine.SwitchState<GameSetUpState>();
        }

        public void OnExit()
        {
            
        }
    }

}