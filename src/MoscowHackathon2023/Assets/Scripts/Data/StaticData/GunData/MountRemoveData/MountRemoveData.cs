using UnityEngine;

namespace Data.StaticData.GunData.MountRemoveData
{
    [CreateAssetMenu(menuName = "StaticData/Gun/MountRemove", fileName = "MountRemoveStaticData")]
    public class MountRemoveData : BaseGunData
    {
        [field: SerializeField] public float DropForce { get; private set; }
    }
}