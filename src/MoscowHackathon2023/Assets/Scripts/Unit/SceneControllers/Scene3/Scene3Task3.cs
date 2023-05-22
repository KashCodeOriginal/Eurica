using Unit.GravityCube;
using UnityEngine;
using UnityEngine.Events;

namespace SceneControllers.Scene3
{
    public class Scene3Task3 : MonoBehaviour
    {
        // На покатой странной стене, под потолком, видны две кнопки.
        // Рядом с кнопками горят красные лампочки. Игроку надо взять куб и кинуть его в обе кнопки по очереди.
        // Когда игрок попадает в кнопку, лампочка загорается зеленым цветом.

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
            if (_gravityButton1._isPressed)
                _pressed1 = true;
            if (_gravityButton2._isPressed)
                _pressed2 = true;

            // Press true forever, do not reset.
            
            if (_pressed1 && _pressed2)
            {
                if (!_isTaskCompleted)
                {
                    Debug.Log("Level completed");
                    _isTaskCompleted = true;
                    OnTaskCompleted?.Invoke();
                }
            }
        }

        private void OnDestroy()
        {
            _gravityButton1.OnStateChanged -= OnStateChanged;
            _gravityButton2.OnStateChanged -= OnStateChanged;
        }
    }
}