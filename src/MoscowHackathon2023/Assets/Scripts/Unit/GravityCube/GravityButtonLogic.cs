using Unit.DoorButton;
using Unit.Player;
using Unit.ScaleGun;
using UnityEngine;

namespace Unit.GravityCube
{
    public class GravityButtonLogic : ButtonLogic
    {
        [SerializeField] private DoorLogic _door;
        [SerializeField] private bool _canPlayerPress;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GravityCubeLogic gravityCubeLogic))
            {
                if (gravityCubeLogic.ColorId == GetColorId())
                {
                    if (!_isPressed)
                    {
                        Press();
                        return;
                    }
                }
            }

            if (other.TryGetComponent(out ScaleCubeMK2 scaleCube))
            {
                if (!_isPressed)
                {
                    Press();
                    return;
                }
            }

            if (other.TryGetComponent(out PlayerMovement player))
            {
                if (_canPlayerPress)
                {
                    if (!_isPressed)
                    {
                        Press();
                        return;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GravityCubeLogic gravityCubeLogic))
            {
                if (gravityCubeLogic.ColorId == GetColorId())
                {
                    if (_isPressed)
                    {
                        Release();
                        return;
                    }
                }
            }

            if (other.TryGetComponent(out ScaleCubeMK2 scaleCube))
            {
                if (_isPressed)
                {
                    Release();
                    return;
                }
            }

            if (other.TryGetComponent(out PlayerMovement player))
            {
                if (_canPlayerPress)
                {
                    if (_isPressed)
                    {
                        Release();
                        return;
                    }
                }
            }
        }

        protected override void Press()
        {
            base.Press();
            _door.Open();
        }

        protected override void Release()
        {
            base.Release();
            _door.Close();
        }
    }
}
