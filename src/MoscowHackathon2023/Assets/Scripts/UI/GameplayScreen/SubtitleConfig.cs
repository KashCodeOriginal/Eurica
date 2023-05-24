using UnityEngine;

namespace UI.GameplayScreen
{
    [CreateAssetMenu(fileName = "SubtitleConfig", menuName = "Subtitle Configuration")]
    public class SubtitleConfig : ScriptableObject
    {
        [System.Serializable]
        public class SpeakerColor
        {
            public string name;
            public Color color;
        }

        public Color defaultColor;
        public SpeakerColor[] speakerColors;
    }
}

