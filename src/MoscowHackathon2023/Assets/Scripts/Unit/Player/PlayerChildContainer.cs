using Cinemachine;
using UnityEngine;

namespace Unit.Player
{
    public class PlayerChildContainer : MonoBehaviour
    {
        [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        public CinemachineInputProvider CinemachineInputProvider => _cinemachineInputProvider;

        public CinemachineVirtualCamera CinemachineVirtualCamera => _cinemachineVirtualCamera;
    }
}
