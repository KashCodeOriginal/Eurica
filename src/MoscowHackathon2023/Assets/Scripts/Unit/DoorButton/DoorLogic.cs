using UnityEngine;
using UnityEngine.Events;

namespace Unit.DoorButton
{
    public class DoorLogic : MonoBehaviour
    {
        [SerializeField] private bool _isOpened;
        public bool IsOpened => _isOpened;

        public UnityAction<bool> OnStateChanged;

        public void Open()
        {
            _isOpened = true;
            OnStateChanged?.Invoke(_isOpened);
        }

        public void Close()
        {
            _isOpened = false;
            OnStateChanged?.Invoke(_isOpened);
        }
    }
}
