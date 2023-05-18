using Data.AssetsAddressablesConstants;
using Infrastructure;
using Infrastructure.ProjectStateMachine.Base;
using UnityEngine.AddressableAssets;

namespace Tools
{
    public class TestState : IState<Bootstrap>, IEnterable
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
    }
}