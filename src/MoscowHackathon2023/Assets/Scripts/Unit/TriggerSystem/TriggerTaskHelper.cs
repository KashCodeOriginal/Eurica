using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using Services.StaticData;
using UI.GameplayScreen;
using UnityEngine;
using Zenject;

namespace Unit.TriggerSystem
{
    public class TriggerTaskHelper : MonoBehaviour
    {
        [Inject]
        public void Construct(Bootstrap bootstrap, IStaticDataService staticDataService)
        {
            _bootstrap = bootstrap;
        }

        private Bootstrap _bootstrap;
        private IStaticDataService _staticDataService;

        public void ShowHint(string hint)
        {
            GameplayScreen.Instance.GameplayHintView.RequestShowingHint(hint);
        }

        public void HideHint()
        {
            GameplayScreen.Instance.GameplayHintView.RequestHidingHint();
        }

        public void ShowTask(string task)
        {
            GameplayScreen.Instance.GameplayTaskView.RequestShowingTask(task);
        }

        public void FailTask()
        {
            GameplayScreen.Instance.GameplayTaskView.RequestTaskFail();
        }

        public void StartMonologue(string speechId)
        {
            var voiceMessage = _staticDataService.GetVoiceMessageByID(speechId);
        }

        public void ChangeScene(string sceneName)
        {
            _bootstrap.StateMachine.SwitchState<GameLoadingState, string>(sceneName);
        }
    }
}
