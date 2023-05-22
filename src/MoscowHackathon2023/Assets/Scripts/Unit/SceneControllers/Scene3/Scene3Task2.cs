using Unit.GravityCube;
using UnityEngine;
using UnityEngine.Events;

namespace SceneControllers.Scene3
{
    public class Scene3Task2 : MonoBehaviour
    {
        // ����� ������ ��� ���� �� ��� ������ ��� ����. � ���� ������ ������� ������������. ������������ �����������.

        [SerializeField] private GravityButtonLogic _gravityButton1;
        [SerializeField] private GravityButtonLogic _gravityButton2;
        private bool _pressed1;
        private bool _pressed2;
        private bool _isTaskCompleted = false;

        public UnityEvent OnTaskCompleted;

        private void Start()
        {
            _gravityButton1.OnStateChanged += OnStateChanged;
            _gravityButton2.OnStateChanged += OnStateChanged;
        }   

        private void OnStateChanged(bool state)
        {
            _pressed1 = _gravityButton1._isPressed;
            _pressed2 = _gravityButton2._isPressed;

            if (_pressed1 && _pressed2)
            {
                if (!_isTaskCompleted)
                {
                    _isTaskCompleted = true;
                    OnTaskCompleted?.Invoke();
                }
            }
        } 
    }
}