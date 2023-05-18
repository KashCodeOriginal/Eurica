using UnityEngine;

namespace Unit.Weapon
{
    internal interface IWeaponedView
    {
        void PickUp(Transform placeInHand);
        void Release();
    }
}