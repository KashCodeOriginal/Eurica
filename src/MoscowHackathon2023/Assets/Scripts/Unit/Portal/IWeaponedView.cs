using UnityEngine;

namespace Unit.Portal
{
    internal interface IWeaponedView
    {
        void PickUp(Transform placeInHand);
        void Release();
    }
}