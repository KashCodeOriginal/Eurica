using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplayHintView : MonoBehaviour
    {
        private string currentHint;

        public void RequestShowingHint(string hint, float time = -1)
        {
            if (currentHint == hint)
                return;

            currentHint = hint;

            Debug.Log("<color=yellow>Show hint: </color>" + hint + " for " + time + " seconds");
            // Queue of hints

            // if -1 then show forever
        }

        public void RequestHidingHint()
        {
            Debug.Log("<color=yellow>Request hiding hint</color>");

            // Hide hint with text from current or queue
        }
    }
}
