using Cinemachine;
using UnityEngine;

namespace Unit.CameraContainer
{
    public class CameraChildContainer : MonoBehaviour
    {
        [SerializeField] private Transform _weaponContainer;
        public Transform WeaponContainer => _weaponContainer;
    }
}
