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
        [SerializeField] private SubtitleConfig _config;
        private VoiceMessage _voiceMessage;
        private bool _subtitlesEnabled;

        private void Awake()
        {
            subtitlesOutput.text = "";
            HideSubtitles();
        }

        public void SetSettings(bool subtitlesEnabled)
        {
            _subtitlesEnabled = subtitlesEnabled;
        }

        public void HideSubtitles()
        {
            subtitlesViewParent.SetActive(false);
            StopAllCoroutines();
        }

        public void ShowSubtitles(VoiceMessage voiceMessage)
        {
            StopAllCoroutines();

            _voiceMessage = voiceMessage;
            StartCoroutine(PlaySubtitles());
        }

        private IEnumerator PlaySubtitles()
        {
            if (!_subtitlesEnabled)
            {
                HideSubtitles();
                yield break;
            }

            subtitlesViewParent.SetActive(true);

            foreach (var phrase in _voiceMessage.Phrases)
            {
                if (phrase.Length <= 0.2f)
                    phrase.Length += 0.2f;

                string text = phrase.Text.Replace("Є", "е");

                text = ConfigureText(text);

                subtitlesOutput.text = text;

                // 0.2f - это врем€ между субтитрами, дл€ большей киношности.
                yield return new WaitForSeconds(phrase.Length - 0.2f);

                subtitlesViewParent.SetActive(false);

                yield return new WaitForSeconds(0.2f);

                subtitlesViewParent.SetActive(true);
            }

            HideSubtitles();
        }
        
        private string ConfigureText(string text)
        {
            string subtitleFormat = "<color=#{0}><b>{1}</b></color><br>{2}";

            int colonIndex = text.IndexOf(':');
            if (colonIndex != -1 && _config != null)
            {
                string speaker = text.Substring(0, colonIndex).ToUpper();
                string dialogue = text.Substring(colonIndex + 1).Trim();

                Color speakerColor = GetSpeakerColor(speaker, _config);

                string colorHex = ColorUtility.ToHtmlStringRGB(speakerColor);
                string formattedSubtitle = string.Format(subtitleFormat, colorHex, speaker, dialogue);

                if (speaker == "ј–“≈ћ" && dialogue.Contains("Ёврика"))
                {
                    dialogue.Replace("Ёврика!", "<color=#FF97EF><b>Ёврика</b></color>");
                }

                return formattedSubtitle;
            }

            return text;
        }

        private Color GetSpeakerColor(string speaker, SubtitleConfig config)
        {
            foreach (SubtitleConfig.SpeakerColor speakerColor in config.speakerColors)
            {
                if (speakerColor.name.ToUpper().Equals(speaker))
                {
                    return speakerColor.color;
                }
            }
            
            return config.defaultColor;
        }
    }
}

