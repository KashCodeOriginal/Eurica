using UnityEngine;

namespace Data.StaticData.GunData.GravityGunData
{
    [CreateAssetMenu(menuName = "StaticData/GravityGun", fileName = "GravityGunStaticData")]
    public class GravityGunData : BaseGunData
    {
        [field: SerializeField] public float CatchDistance { get; private set; }
        [field: SerializeField] public float CatchPower { get; private set; }
        [field: SerializeField] public float DropPower { get; private set; }
    }
}