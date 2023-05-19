using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Data.StaticData.LevelData.NPCData
{
    [Serializable]
    public class NPC
    {
        [field: SerializeField] public GameObject NPCPrefab { get; private set; }
        [field: SerializeField] public Vector3 NPCSpawnPoint { get; private set; }
    }
}