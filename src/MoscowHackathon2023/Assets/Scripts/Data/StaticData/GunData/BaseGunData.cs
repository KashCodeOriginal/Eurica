using Unit.UniversalGun;
using UnityEngine;

namespace Data.StaticData.GunData
{
        [CreateAssetMenu(menuName = "StaticData/BaseGun", fileName = "BaseGunStaticData")]
        public class BaseGunData : ScriptableObject 
        {
            [field: SerializeField] public Sprite InventoryIcon { get; private set; }
            [field: SerializeField] public int IndexWeapon { get; private set; }
            [field: SerializeField] public GunTypes GunType { get; private set; }
            
            [field: SerializeField] public AudioClip FirstGunSound { get; private set; }
            [field: SerializeField] public AudioClip SecondGunSound { get; private set; }
        }
}