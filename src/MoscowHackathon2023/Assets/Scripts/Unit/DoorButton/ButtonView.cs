using UnityEngine;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private ButtonLogic _buttonLogic;
    [SerializeField] private Transform _buttonMesh;
    [SerializeField] private Vector3 _localPosUnpressed;
    [SerializeField] private Vector3 _localPosPressed;

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
        _buttonMesh.localPosition = state ? _localPosPressed : _localPosUnpressed;
    }
}
