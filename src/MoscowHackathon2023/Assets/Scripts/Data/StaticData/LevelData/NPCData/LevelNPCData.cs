using System.Collections.Generic;
using UnityEngine;

namespace Data.StaticData.LevelData.NPCData
{
    [CreateAssetMenu(menuName = "StaticData/Level/NPCOnLevel", fileName = "NPCOnLevel")]
    public class LevelNPCData : ScriptableObject
    {
        [field: SerializeField] public string LevelName { get; private set; }
        [field: SerializeField] public List<NPC> NPCOnLevel { get; private set; }
    }
}