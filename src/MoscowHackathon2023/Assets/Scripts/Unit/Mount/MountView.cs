using Cinemachine;
using Pathfinding;
using UnityEngine;

namespace Unit.Mount
{
    public class MountView : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _mountCamera;
        [SerializeField] private GameObject _targetPrefab;
        [SerializeField] private AIDestinationSetter _aiDestinationSetter;
        
        private Transform _target;

        public CinemachineVirtualCamera MountCamera => _mountCamera;

        public Transform Target => _target;

        public GameObject TargetPrefab => _targetPrefab;

        public void SetTarget(Transform target)
        {
            _target = target;
            
            _aiDestinationSetter.target = _target;
        }
    }
}
