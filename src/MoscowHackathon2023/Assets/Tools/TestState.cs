using Data.AssetsAddressablesConstants;
using Infrastructure.ProjectStateMachine.Base;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class TestState : IState<Bootstrap>, IEnterable, IExitable
    {
        public Bootstrap Initializer { get; }

        public TestState()
        {            
            
        }

        public async void OnEnter()
        {
            var asyncOperationHandler = Addressables.LoadSceneAsync(AssetsAddressablesConstants.TEST_LEVEL);
            await asyncOperationHandler.Task;            
        }

        public void OnExit()
        {
            
        }
    }
}