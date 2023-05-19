using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using UI.GameplayScreen;
using UnityEngine;
using Zenject;

namespace Unit.TriggerSystem
{
    public class TriggerTaskHelper : MonoBehaviour
    {
        [Inject]
        public void Construct(Bootstrap bootstrap)
        {
            _bootstrap = bootstrap;
        }

        private Bootstrap _bootstrap;

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
            // TODO: Start monologue with audio file and subtitles in a custom system.
        }

        public void ChangeScene(string sceneName)
        {
            _bootstrap.StateMachine.SwitchState<GameLoadingState, string>(sceneName);
        }
    }
}
