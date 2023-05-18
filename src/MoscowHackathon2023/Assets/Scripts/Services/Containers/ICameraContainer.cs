using UnityEngine;

namespace Services.Containers
{
    public interface ICameraContainer
    {
        public Camera Camera { get; }
        public void SetUpCamera(Camera camera);
    }
}