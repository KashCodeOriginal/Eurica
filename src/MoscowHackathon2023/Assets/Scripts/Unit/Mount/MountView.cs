using System;
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
        [SerializeField] private MountAnimator _mountAnimator;
        [SerializeField] private AIPath _aiPath;
        
        private Transform _target;

        public CinemachineVirtualCamera MountCamera => _mountCamera;

        public Transform Target => _target;

        public GameObject TargetPrefab => _targetPrefab;

        private void Update()
        {
            if (_aiPath.remainingDistance <= 0.5f)
            {
                _mountAnimator.SetWalkingAnimation(false);
                return;
            }  
            
            _mountAnimator.SetWalkingAnimation(true);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            
            _aiDestinationSetter.target = _target;
        }
    }
}
