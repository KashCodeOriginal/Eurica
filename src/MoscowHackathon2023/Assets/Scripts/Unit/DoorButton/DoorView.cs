using UnityEngine;

public class DoorVisuals : MonoBehaviour
{
    [SerializeField] private DoorLogic _doorLogic;
    [SerializeField] private Transform _movingPart;

    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector3 _localPosOpened;
    [SerializeField] private Vector3 _localPosClosed;
    private Vector3 _targetPosition;

    private void Start()
    {
        OnStateChanged(_doorLogic.IsOpened);
    }

    private void OnEnable()
    {
        _doorLogic.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _doorLogic.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(bool state)
    {
        _targetPosition = state ? _localPosOpened : _localPosClosed;
    }

    private void Update()
    {
        if (_movingPart.localPosition == _targetPosition)
            return;

        _movingPart.localPosition = Vector3.Lerp(_movingPart.localPosition, _targetPosition, _speed * Time.deltaTime);
    }
}
