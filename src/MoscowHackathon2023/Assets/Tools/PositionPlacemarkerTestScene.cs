using UnityEngine;
using UnityEngine.Serialization;

namespace Tools
{
    public class PositionPlacemarkerTestScene : MonoBehaviour 
    {
        [SerializeField] private Transform _placeSpawnPortalGun;
        [SerializeField] private Transform _placeSpawnGravityGun;
        [SerializeField] private Transform _placeSpawnScaleGun;
        [SerializeField] private Transform _placeSpawnMountRemote;

        public Transform PlaceSpawnPortalGun { get => _placeSpawnPortalGun; }
        public Transform PlaceSpawnGravityGun { get => _placeSpawnGravityGun; }
        public Transform PlaceSpawnScaleGun { get => _placeSpawnScaleGun; }
        public Transform PlaceSpawnMountRemove { get => _placeSpawnMountRemote; }
    }
}