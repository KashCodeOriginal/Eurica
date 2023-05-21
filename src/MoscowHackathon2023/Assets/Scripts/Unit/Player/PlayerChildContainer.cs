using Cinemachine;
using UnityEngine;

namespace Unit.Player
{
    public class PlayerChildContainer : MonoBehaviour
    {
        [SerializeField] private CinemachineInputProvider _cinemachineInputProvider;

        public CinemachineInputProvider CinemachineInputProvider => _cinemachineInputProvider;   
    }
}
