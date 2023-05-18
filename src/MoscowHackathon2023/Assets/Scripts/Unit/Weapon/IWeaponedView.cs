using UnityEngine;

namespace Unit.Weapon
{
    internal interface IWeaponedView
    {
        public void PickUp();
        public void Release();
    }
}