using Cinemachine;
using UnityEngine;

namespace Services.Containers
{
    public interface ICameraContainer
    {
        public Camera Camera { get; }
        public CinemachineBrain CinemachineBrain { get; }
        public void SetUpCamera(Camera camera, CinemachineBrain cinemachineBrain);
    }
}