using System;
using UnityEngine;

namespace Unit.ScaleGun
{
    public class Scalable : MonoBehaviour, IScalable
    {
        [SerializeField] private float _minScale;
        [SerializeField] private float _maxScale;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0, 0, 0.2f);
                
            Gizmos.DrawCube(transform.position + new Vector3(0, _maxScale / 2 - 0.5f, 0) , 
                Vector3.one * _maxScale);
        }

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