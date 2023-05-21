using UnityEngine;

namespace Data.StaticData.LevelData
{
    [CreateAssetMenu(menuName = "StaticData/Level/LevelData", fileName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public string LevelName { get; private set; }
        [field: SerializeField] public Vector3 PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Quaternion PlayerSpawnRotation { get; private set; }
    }
}