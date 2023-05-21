using Unit.DoorButton;
using UnityEngine;

namespace Unit.Lever
{
    public class LeverView : MonoBehaviour
    {
        [SerializeField] private LeverButtonLogic _leverLogic;
        [SerializeField] private Transform _movingPart;

        [Header("Press Movement")]
        [SerializeField] private Transform _transformPressed;
        [SerializeField] private Transform _transformUnpressed;
        private Transform _targetTransform;
        private float _speed;

        private void Start()
        {
            OnStateChanged(false);
        }

        private void OnEnable()
        {
            _leverLogic.OnStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _leverLogic.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(bool state)
        {
            _targetTransform = state ? _transformPressed : _transformUnpressed;
        }


        private void Update()
        {
            _speed = _leverLogic.GetSpeed();

            if (_movingPart.position == _targetTransform.position
                && _movingPart.rotation == _targetTransform.rotation)
                return;

            _movingPart.localPosition = Vector3.Lerp(_movingPart.localPosition, _targetTransform.localPosition, _speed * Time.deltaTime);
            _movingPart.localRotation = Quaternion.Lerp(_movingPart.localRotation, _targetTransform.localRotation, _speed * Time.deltaTime);
        }
    }
}