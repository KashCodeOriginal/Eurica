using TMPro;
using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplayTaskView : MonoBehaviour
    {
        private string currentTask;

        [SerializeField] private GameObject taskViewParent;
        [SerializeField] private TextMeshProUGUI taskOutput;

        private void Awake()
        {
            taskOutput.text = "";
            RequestHidingTask();
        }

        public void RequestShowingTask(string task)
        {
            if (currentTask == task)
                return;

            currentTask = task;
            taskOutput.text = task;
            taskViewParent.SetActive(true);
        }

        public void RequestHidingTask()
        {
            taskOutput.text = "";
            currentTask = "";
            taskViewParent.SetActive(false);
        }

        public void RequestTaskFail()
        {
            // Show "Task failed" text
            // Hide task
        }
    }
}