using UI.GameplayScreen;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace TriggerSystem
{
    public class TriggerTaskHelper : MonoBehaviour
    {
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
    }
}
