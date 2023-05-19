using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data.StaticData.LevelData.NPCData
{
    [Serializable]
    public class NPC
    {
        [field: SerializeField] public AssetReference NPCPrefab { get; private set; }
        [field: SerializeField] public Vector3 NPCSpawnPoint { get; private set; }
    }
}