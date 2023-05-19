using UnityEngine;
using UnityEngine.Events;

namespace Unit.DoorButton
{
    public class ButtonLogic : MonoBehaviour
    {
        protected bool _isPressed = false;
        public UnityAction<bool> OnStateChanged;

        [SerializeField] private GravityCubeSettings _buttonGravitySettings;
        [SerializeField] private int _colorId;
        public int GetColorId() => _colorId;
        [SerializeField] private GravityCubeLogic _gravityCube;

        private void Start()
        {
            _gravityCube.Init(_colorId, GetCubeColor());
        }

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

        public Color GetCubeColor()
        {
            if (_colorId < 0 || _colorId >= _buttonGravitySettings.ColorVariants.Length)
            {
                Debug.LogError("Color id is out of range in _buttonGravitySettings");
                return Color.white;
            }

            return _buttonGravitySettings.ColorVariants[_colorId];
        }
    }
}
