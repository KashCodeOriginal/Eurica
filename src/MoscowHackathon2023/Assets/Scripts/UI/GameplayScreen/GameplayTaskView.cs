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
            RequestHidingTask();
        }

        public void RequestShowingTask(string task)
        {
            if (currentTask == task)
                return;

            currentTask = task;
            taskOutput.text = task;
            taskViewParent.SetActive(true);

            Debug.Log("<color=cyan>Show task: </color>" + task);
        }

        public void RequestHidingTask()
        {
            taskOutput.text = "";
            currentTask = "";
            taskViewParent.SetActive(false);

            Debug.Log("<color=cyan>Request hiding task </color>");
        }

        public void RequestTaskFail()
        {
            Debug.Log("<color=cyan>Request task fail</color>");

            // Show "Task failed" text
            // Hide task
        }
    }
}