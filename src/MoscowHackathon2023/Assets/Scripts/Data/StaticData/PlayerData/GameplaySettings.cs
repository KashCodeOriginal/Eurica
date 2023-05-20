using UnityEngine;

namespace Data.StaticData.PlayerData
{
    [CreateAssetMenu(menuName = "StaticData/Game/GameplaySettings", fileName = "GameplaySettings")]
    public class GameplaySettings : ScriptableObject
    {
#if UNITY_EDITOR
        [field: SerializeField] public float SoundVolume = 0.3f;
        [field: SerializeField] public float MouseSens = 5f;
        [field: SerializeField] public bool Subtitles = true;
#else
        // Use different values for release build
        public float SoundVolume = 0.5f;
        public float MouseSens = 6f;
        public bool Subtitles = true;
#endif
    }
}