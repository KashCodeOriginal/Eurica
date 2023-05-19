using System.Collections.Generic;
using UnityEngine;

namespace Data.StaticData.VoicePhrases
{
    [CreateAssetMenu(menuName = "StaticData/Voice/VoiceMessage", fileName = "VoiceMessage")]
    public class VoiceMessage : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public List<Phrase> Phrases { get; private set; }
    }
}