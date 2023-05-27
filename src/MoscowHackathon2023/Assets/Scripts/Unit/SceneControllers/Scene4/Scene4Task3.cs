using Services.GameProgress;
using Unit.GravityCube;
using Unit.ScaleGun;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Unit.SceneControllers.Scene4
{
    public class Scene4Task3 : MonoBehaviour
    {
        public UnityEvent OnLevelCompleted;
        private bool _isLevelCompleted;
        [SerializeField] private QuizLampLogic _quizLogic;

        private IGameProgressService _gameProgressService;

        private bool _isCubeEntered;
        private bool _isAllRed;

        [Inject]
        public void Construct(IGameProgressService gameProgressService)
        {
            _gameProgressService = gameProgressService;
        }

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
            // Set visuals for a display
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
                    
                    _gameProgressService.SetUpHubStage(HubStage.Third);
                    _gameProgressService.SetUpLiftStage(LiftStage.Third);
                    
                    OnLevelCompleted?.Invoke();
                }
            }
        }
    }
}
