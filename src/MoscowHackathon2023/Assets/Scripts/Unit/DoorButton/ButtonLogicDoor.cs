using System;
using UnityEngine;

namespace Unit.DoorButton
{
    public class ButtonLogicDoor : ButtonLogic
    {
        [SerializeField] private DoorLogic _door;

        private void OnTriggerEnter(Collider other)
        {
            // Detect Gravity Cube colission
            if (other.gameObject.layer == LayerMask.NameToLayer("InteractiveObjectForGravity") 
                || other.gameObject.layer == LayerMask.NameToLayer("Grabbed"))
            {
                if (!_isPressed)
                {
                    Press();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("InteractiveObjectForGravity") 
                || other.gameObject.layer == LayerMask.NameToLayer("Grabbed"))
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
