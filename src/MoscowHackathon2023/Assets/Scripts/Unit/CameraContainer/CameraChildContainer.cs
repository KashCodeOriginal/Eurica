using UnityEngine;

namespace Unit.CameraContainer
{
    public class CameraChildContainer : MonoBehaviour
    {
        [SerializeField] private Transform _inventoryContainer;
        [SerializeField] private Transform _weaponContainer;

        public Transform InventoryContainer => _inventoryContainer;

        public Transform WeaponContainer => _weaponContainer;
    }
}
