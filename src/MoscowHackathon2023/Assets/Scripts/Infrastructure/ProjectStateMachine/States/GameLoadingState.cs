using Data.AssetsAddressablesConstants;
using Infrastructure.ProjectStateMachine.Base;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameLoadingState : IState<Bootstrap>, IEnterable, IExitable
    {
        public Bootstrap Initializer { get; }
        
        public GameLoadingState(Bootstrap initializer)
        {
            Initializer = initializer;
        }

        public async void OnEnter()
        {
            var asyncOperationHandler = Addressables.LoadSceneAsync(AssetsAddressablesConstants.GAMEPLAY_LEVEL);

            await asyncOperationHandler.Task;
            
            Initializer.StateMachine.SwitchState<GameSetUpState>();
        }

        public void OnExit()
        {
            
        }
    }
}