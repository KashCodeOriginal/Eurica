using Unit.GravityCube;
using UnityEngine;

namespace Unit.DoorButton
{
    public class ButtonLogicDoor : ButtonLogic
    {
        [SerializeField] private DoorLogic _door;

        private void OnTriggerEnter(Collider other)
        {
            // Detect Gravity Cube colission

            GravityCubeLogic gravityCube = other.GetComponent<GravityCubeLogic>();
            if (gravityCube != null && gravityCube._colorId == base.GetColorId())
            {
                if (!_isPressed)
                {
                    Press();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            GravityCubeLogic gravityCube = other.GetComponent<GravityCubeLogic>();
            if (gravityCube != null && gravityCube._colorId == base.GetColorId())
            {
                if (_isPressed)
                {
                    Release();
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
