using UnityEngine;

public class DoorVisuals : MonoBehaviour
{
    [SerializeField] private DoorLogic _doorLogic;
    [SerializeField] private Transform _doorMesh;
    [SerializeField] private Vector3 _localPosClosed;
    [SerializeField] private Vector3 _localPosOpened;

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
        _doorMesh.localPosition = state ? _localPosOpened : _localPosClosed;
    }
}
