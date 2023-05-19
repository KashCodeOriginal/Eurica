using UnityEngine;
using UnityEngine.Events;

public class ButtonLogic : MonoBehaviour
{
    private bool _isPressed = false;
    public UnityAction<bool> OnStateChanged;

    private void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Press()
    {
        _isPressed = true;
        OnStateChanged?.Invoke(_isPressed);
    }
}
