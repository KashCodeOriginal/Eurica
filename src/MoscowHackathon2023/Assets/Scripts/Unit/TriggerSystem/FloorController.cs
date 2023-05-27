using System;
using Services.Containers;
using Unit.Player;
using Unit.ScaleGun;
using UnityEngine;
using Zenject;

namespace Unit.TriggerSystem
{
    public class FloorController : MonoBehaviour
    {
        [SerializeField] private Vector3 _cubeSpawnPoint;
        
        private Vector3 _currentCheckPoint;

        private IGameInstancesContainer _gameInstancesContainer;

        [Inject]
        private void Construct(IGameInstancesContainer gameInstancesContainer)
        {
            _gameInstancesContainer = gameInstancesContainer;
        }
        
        public void SetUpCurrentCheckPoint(TriggerReturnPosition checkPoint)
        {
            _currentCheckPoint = checkPoint.ReturnPoint.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
               // _gameInstancesContainer.TurnOffPlayer();
                
                other.transform.position = _currentCheckPoint;

               // _gameInstancesContainer.TurnOnPlayer();
            }

            if (other.TryGetComponent(out ScaleCubeMK2 _))
            {
                other.transform.localPosition = _cubeSpawnPoint;
            }
        }
    }
}
