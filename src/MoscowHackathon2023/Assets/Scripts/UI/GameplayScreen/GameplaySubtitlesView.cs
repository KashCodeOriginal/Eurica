using Data.StaticData.VoicePhrases;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplaySubtitlesView : MonoBehaviour
    {
        private string currentHint;

        [SerializeField] private GameObject subtitlesViewParent;
        [SerializeField] private TextMeshProUGUI subtitlesOutput;
        private VoiceMessage _voiceMessage;

        private void Awake()
        {
            RequestHidingSubtitles();
        }

        public void RequestShowSubtitles(VoiceMessage voiceMessage)
        {
            StopAllCoroutines();

            _voiceMessage = voiceMessage;
            StartCoroutine(PlaySubtitles());
        }

        private IEnumerator PlaySubtitles()
        {
            subtitlesViewParent.SetActive(true);

            foreach (var phrase in _voiceMessage.Phrases)
            {
                if (phrase.Length <= 0.5f)
                    Debug.LogError($"Phrase length is {phrase.Length} seconds, make it longer for ID {_voiceMessage.ID} in {phrase.Text}");

                subtitlesOutput.text = phrase.Text;
                
                yield return new WaitForSeconds(phrase.Length - 0.2f);

                subtitlesViewParent.SetActive(false);

                yield return new WaitForSeconds(0.2f);

                subtitlesViewParent.SetActive(true);
            }

            subtitlesViewParent.SetActive(false);
        }

        public void RequestHidingSubtitles()
        {
            subtitlesViewParent.SetActive(false);
        }
    }
}
