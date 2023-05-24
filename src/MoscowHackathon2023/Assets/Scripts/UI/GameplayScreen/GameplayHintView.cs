using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplayHintView : MonoBehaviour
    {
        private string currentHint;

        [SerializeField] private GameObject hintViewParent;
        [SerializeField] private TextMeshProUGUI hintOutput;

        private void Awake()
        {
            hintOutput.text = "";
            RequestHidingHint();
        }

        public void RequestShowingHint(string hint)
        {
            if (currentHint == hint)
                return;

            currentHint = hint;
            hintOutput.text = hint;
            hintViewParent.SetActive(true);

            Debug.Log("<color=yellow>Show hint: </color>" + hint);
        }

        public void RequestHidingHint()
        {
            hintOutput.text = "";
            currentHint = "";
            hintViewParent.SetActive(false);

            Debug.Log("<color=yellow>Request hiding hint</color>");
        }
    }
}
