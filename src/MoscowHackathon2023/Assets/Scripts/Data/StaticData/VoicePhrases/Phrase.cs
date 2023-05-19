using System;
using UnityEngine;

namespace Data.StaticData.VoicePhrases
{
    [Serializable]
    public class Phrase
    {
        [field: SerializeField] public string PhraseText { get; private set; }
        [field: SerializeField] public float PhraseLength { get; private set; }
    }
}