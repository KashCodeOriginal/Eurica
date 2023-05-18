using Services.Input;
using UnityEngine;

namespace Unit.ScaleGun
{
    public class ScaleGun
    {
        public ScaleGun(PlayerInputActionReader playerInputActionReader, Camera camera)
        {
            _playerInputActionReader = playerInputActionReader;

            _camera = camera;

            _playerInputActionReader.IsLeftButtonClicked += TryScale;

            _playerInputActionReader.IsMouseScroll += OnMouseScroll;
        }

        private PlayerInputActionReader _playerInputActionReader;

        private Camera _camera;
        
        private IScalable _currentScalableObject;

        private float _resizeValue = 0.05f;


        public void TryScale()
        {
            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            if (!hit.collider.TryGetComponent(out IScalable scalable))
            {
                return;
            }

            _currentScalableObject = scalable;
        }

        private void OnMouseScroll(float onMouseScroll)
        {
            if (_currentScalableObject == null)
            {
                return;
            }
            
            if (onMouseScroll > 0)
            {
                _currentScalableObject.UpScale(_resizeValue);
            }
            else if (onMouseScroll < 0)
            {
                _currentScalableObject.DownScale(_resizeValue);
            }
        }
    }
}