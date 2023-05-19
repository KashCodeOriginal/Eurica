using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplayTaskView : MonoBehaviour
    {
        private string currentTask;

        public void RequestShowingTask(string task)
        {
            if (currentTask == task)
                return;

            currentTask = task;

            Debug.Log("<color=cyan>Show task: </color>" + task);
            // Show task text
        }

        public void RequestTaskFail()
        {
            Debug.Log("<color=cyan>Request task fail</color>");

            // Show "Task failed" text
            // Hide task
        }
    }
}