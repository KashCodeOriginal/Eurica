using UnityEngine;

namespace Data.StaticData.PlayerData
{
    [CreateAssetMenu(menuName = "StaticData/Game/GameplaySettings", fileName = "GameplaySettings")]
    public class GameplaySettings : ScriptableObject
    {
        [field: SerializeField] public float SoundVolume = 0.3f;
        [field: SerializeField] public float MouseSens = 15f;
        [field: SerializeField] public bool Subtitles = true;
    }
}