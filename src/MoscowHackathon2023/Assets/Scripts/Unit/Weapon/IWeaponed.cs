using Data.StaticData.GunData;
using Unit.UniversalGun;

namespace Unit.Weapon
{
    public interface IWeaponed
    {
        public void SetUpUniversalView(UniversalGunView universalGunView);
        public BaseGunData GunData { get; }
        public void Select();
        public void Deselect();
    }
}