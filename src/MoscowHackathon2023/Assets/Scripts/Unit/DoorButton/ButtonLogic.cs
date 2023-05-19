using UnityEngine;
using UnityEngine.Events;

namespace Unit.DoorButton
{
    public class ButtonLogic : MonoBehaviour
    {
        protected bool _isPressed = false;
        public UnityAction<bool> OnStateChanged;

        protected virtual void Press()
        {
            _isPressed = true;
            OnStateChanged?.Invoke(_isPressed);
        }

        protected virtual void Release()
        {
            _isPressed = false;
            OnStateChanged?.Invoke(_isPressed);
        }
    }
}
