using UnityEngine;

namespace Unit.ScaleGun
{
    public class Scalable : MonoBehaviour, IScalable
    {
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;

        public GameObject GameObject => gameObject;

        public void UpScale(float resizeValue)
        {
            if (transform.localScale.x >= _maxScale)
            {
                return;
            }
            
            transform.localScale = transform.localScale += new Vector3(resizeValue, resizeValue, resizeValue);
        }

        public void DownScale(float resizeValue)
        {
            if (transform.localScale.x <= _minScale)
            {
                return;
            }

            transform.localScale = transform.localScale -= new Vector3(resizeValue, resizeValue, resizeValue);
        }
    }
}