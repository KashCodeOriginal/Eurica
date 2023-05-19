using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.StaticData.LevelData.NPCData
{
    [Serializable]
    public class LevelNPCData
    {
        [field: SerializeField] public List<NPC> NPCOnLevel { get; private set; }
    }
}