using Cinemachine;
using UnityEngine;

namespace Services.Containers
{
    public class CameraContainer : ICameraContainer
    {
        public Camera Camera { get; private set; }
        public CinemachineBrain CinemachineBrain { get; private set; }
        
        public void SetUpCamera(Camera camera, CinemachineBrain cinemachineBrain)
        {
            Camera = camera;
            CinemachineBrain = cinemachineBrain;
        }
    }
}