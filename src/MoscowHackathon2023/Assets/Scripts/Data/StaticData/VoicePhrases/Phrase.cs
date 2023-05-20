using System;
using UnityEngine;

namespace Data.StaticData.VoicePhrases
{
    [Serializable]
    public class Phrase
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public float Length { get; private set; }
    }
}