using UnityEngine;

namespace Data.StaticData.PlayerData
{
    [CreateAssetMenu(menuName = "StaticData/Player/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerBaseSettings : ScriptableObject
    {
        [field: SerializeField] public float ForwardWalkSpeed { get; private set; }
        [field: SerializeField] public float BackwardWalkSpeed { get; private set; }
        [field: SerializeField] public float SideWalkSpeed { get; private set; }
        
        [field: SerializeField] public float RunMultiplier { get; private set; }
        [field: SerializeField] public float JumpPower { get; private set; }
        [field: SerializeField] public AudioClip WalkSound { get; private set; }
        [field: SerializeField] public float WalkSoundFrequency { get; private set; }
    }
}