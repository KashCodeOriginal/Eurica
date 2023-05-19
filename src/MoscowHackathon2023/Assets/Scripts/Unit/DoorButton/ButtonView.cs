using UnityEngine;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private ButtonLogic _buttonLogic;
    [SerializeField] private Transform _movingPart;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector3 _localPosUnpressed;
    [SerializeField] private Vector3 _localPosPressed;
    private Vector3 _targetPosition;

    private void Start()
    {
        OnStateChanged(false);
    }

    private void OnEnable()
    {
        _buttonLogic.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _buttonLogic.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(bool state)
    {
        _targetPosition = state ? _localPosPressed : _localPosUnpressed;
    }


    private void Update()
    {
        if (_movingPart.localPosition == _targetPosition)
            return;

        _movingPart.localPosition = Vector3.Lerp(_movingPart.localPosition, _targetPosition, _speed * Time.deltaTime);
    }
}
