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

        [Header("Visual Screen")]
        [SerializeField] private GameObject _cubeNotReady;
        [SerializeField] private GameObject _cubeReady;
        [SerializeField] private GameObject _lampsNotReady;
        [SerializeField] private GameObject _lampsReady;
        [SerializeField] private GameObject _pullLever;
        [SerializeField] private GameObject _separator;
        [SerializeField] private GameObject _liftUnlocked;
        [SerializeField] private GameObject _arrowHelper;

        [Inject]
        public void Construct(IGameProgressService gameProgressService)
        {
            _gameProgressService = gameProgressService;
        }

        private void OnEnable()
        {
            _quizLogic.OnLampChanged += OnLampChanged;

            StateChanged();
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
                cube.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                cube.gameObject.GetComponent<BoxCollider>().enabled = false;
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
            if (!_isLevelCompleted)
            {
                _arrowHelper.SetActive(!_isCubeEntered);
                _cubeNotReady.SetActive(!_isCubeEntered);
                _cubeReady.SetActive(_isCubeEntered);
                _lampsNotReady.SetActive(!_isAllRed);
                _lampsReady.SetActive(_isAllRed);

                _pullLever.SetActive(_isAllRed && _isCubeEntered);
                _liftUnlocked.SetActive(false);
            }
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

                    _cubeNotReady.SetActive(false);
                    _cubeReady.SetActive(false);
                    _lampsNotReady.SetActive(false);
                    _lampsReady.SetActive(false);
                    _pullLever.SetActive(false);
                    _separator.SetActive(false);
                    _liftUnlocked.SetActive(true);

                    _gameProgressService.SetUpHubStage(HubStage.Third);
                    _gameProgressService.SetUpLiftStage(LiftStage.Third);
                    
                    OnLevelCompleted?.Invoke();
                }
            }
        }
    }
}
