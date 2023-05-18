using UnityEngine;

namespace Data.StaticData.GunData.ScaleGunData
{
    [CreateAssetMenu(menuName = "StaticData/Gun/ScaleGun", fileName = "ScaleGunStaticData")]
    public class ScaleGunData : BaseGunData
    {
        [field: SerializeField] public float ResizeValue { get; private set; }
    }
}