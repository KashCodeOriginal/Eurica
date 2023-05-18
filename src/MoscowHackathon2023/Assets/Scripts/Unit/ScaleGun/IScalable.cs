using UnityEngine;

namespace Unit.ScaleGun
{
    public interface IScalable
    {
        public GameObject GameObject { get; }
        public void UpScale(float resizeValue);
        public void DownScale(float resizeValue);
    }
}