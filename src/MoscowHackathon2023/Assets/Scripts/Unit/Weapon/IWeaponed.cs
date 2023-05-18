using Data.StaticData.GunData;

namespace Unit.Weapon
{
    public interface IWeaponed
    {
        public BaseGunData GunData { get; }
        public void PickUp();
        public void Release();
    }
}