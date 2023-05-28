using Unit.Player;
using Unit.ScaleGun;
using UnityEngine;

namespace Unit.TriggerSystem
{
    public class FloorController : MonoBehaviour
    {
        [SerializeField] private Vector3 _cubeSpawnPoint;
        
        private Vector3 _currentCheckPoint;
        
        
        public void SetUpCurrentCheckPoint(TriggerReturnPosition checkPoint)
        {
            _currentCheckPoint = checkPoint.ReturnPoint.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement _))
            {
                other.transform.position = _currentCheckPoint;
            }

            if (other.TryGetComponent(out ScaleCubeMK2 _))
            {
                other.transform.localPosition = _cubeSpawnPoint;
            }
        }
    }
}
