using UnityEngine;

namespace Zenject.Installers
{
    public class PositionPlacemarkerTestScene : MonoBehaviour {
        [SerializeField] private Transform _placeSpawnPortalGun;
        [SerializeField] private Transform _placeSpawnGrawityGun;

        public Transform PlaceSpawnPortalGun { get => _placeSpawnPortalGun; }
        public Transform PlaceSpawnGrawityGun { get => _placeSpawnGrawityGun; }
    }
}