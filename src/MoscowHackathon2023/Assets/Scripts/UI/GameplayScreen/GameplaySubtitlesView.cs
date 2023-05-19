using TMPro;
using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplaySubtitlesView : MonoBehaviour
    {
        private string currentHint;

        [SerializeField] private GameObject subtitlesViewParent;
        [SerializeField] private TextMeshProUGUI subtitlesOutput;

        private void Awake()
        {
            RequestHidingSubtitles();
        }

        public void RequestShowSubtitles()
        {
            // TODO: Request entire class for subtitles and timings.
        }

        public void RequestHidingSubtitles()
        {
            subtitlesViewParent.SetActive(false);
        }
    }
}
