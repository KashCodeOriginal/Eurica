using UnityEngine;

namespace Services.Containers
{
    public class CameraContainer : ICameraContainer
    {
        public Camera Camera { get; private set; }

        public void SetUpCamera(Camera camera)
        {
            Camera = camera;
        }
    }
}