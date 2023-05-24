using Unit.GravityCube;
using Unit.ScaleGun;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.SceneControllers.Scene4
{
    public class Scene4Task3 : MonoBehaviour
    {
        public UnityEvent OnLevelCompleted;
        private bool _isLevelCompleted;
        [SerializeField] private QuizLampLogic _quizLogic;

        private bool _isCubeEntered;
        private bool _isAllRed;

        private void OnEnable()
        {
            _quizLogic.OnLampChanged += OnLampChanged;
        }

        private void OnDisable()
        {
            _quizLogic.OnLampChanged -= OnLampChanged;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Scalable cube))
            {
                _isCubeEntered = true;
                StateChanged();
            }
        }

        private void OnLampChanged()
        {
            _isAllRed = _quizLogic.IsAllRed();
            StateChanged();
        }

        private void StateChanged()
        {
            // Set visuals for a display.

            Debug.Log($"<color=orange>{_isCubeEntered} and {_isAllRed}</color>");
        }

        public void PullLever()
        {
            CheckForLevelCompletion();
        }

        private void CheckForLevelCompletion()
        {
            if (_isAllRed && _isCubeEntered)
            {
                if (!_isLevelCompleted)
                {
                    _isLevelCompleted = true;
                    OnLevelCompleted?.Invoke();
                }
            }
        }
    }
}
