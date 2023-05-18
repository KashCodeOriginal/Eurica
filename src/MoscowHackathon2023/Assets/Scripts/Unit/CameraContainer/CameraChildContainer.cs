using UnityEngine;

namespace Unit.CameraContainer
{
    public class CameraChildContainer : MonoBehaviour
    {
        [SerializeField] private Transform _inventoryContainer;

        public Transform InventoryContainer => _inventoryContainer;
    }
}
