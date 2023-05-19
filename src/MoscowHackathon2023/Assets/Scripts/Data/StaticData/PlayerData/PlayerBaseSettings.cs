using UnityEngine;

namespace Data.StaticData.PlayerData
{
    [CreateAssetMenu(menuName = "StaticData/Player/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerBaseSettings : ScriptableObject
    {
        [field: SerializeField] public float ForwardWalkSpeed { get; private set; }
        [field: SerializeField] public float BackwardWalkSpeed { get; private set; }
        [field: SerializeField] public float SideWalkSpeed { get; private set; }
        
        [field: SerializeField] public float CrouchWalkSpeed { get; private set; }

        [field: SerializeField] public float RunMultiplier { get; private set; }
        
        [field: SerializeField] public float Stamina { get; private set; }
        
        [field: SerializeField] public float StaminaWaste { get; private set; }
        
        [field: SerializeField] public float StaminaRecovery { get; private set; }

        [field: SerializeField] public float JumpPower { get; private set; }
        
    }
}