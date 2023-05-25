using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data.StaticData.BlinkSystem
{
    public class BlinkSystem : MonoBehaviour
    {
        public static BlinkSystem Instance;

        [SerializeField] private Vector2 _blinkPosition;
        [SerializeField] private Vector2 _openedPosition;
        [SerializeField] private float _blinkPauseTime = 0.3f;
        public float GetPauseTime => _blinkPauseTime;

        private Vector2 _targetPosition;
        private Vector2 _newPosition;
        [SerializeField] private float _blinkSpeed;
        [SerializeField] private bool _needToOpenEyes;

        [SerializeField] private RectTransform _eyelidUp;
        [SerializeField] private RectTransform _eyelidDown;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            _eyelidUp.anchoredPosition = _openedPosition;
            _eyelidDown.anchoredPosition = -_openedPosition;
            _targetPosition = _openedPosition;
        }

        public void BlinkAndOpen(float timer = 1f)
        {
            CloseEyelids();
            Invoke(nameof(OpenEyelids), timer);
        }

        public void OpenEyelids()
        {
            _targetPosition = _openedPosition;
            _needToOpenEyes = false;
        }

        public void CloseEyelids()
        {
            _targetPosition = _blinkPosition;
            _needToOpenEyes = true;
        }

        private void Update()
        {
            if (_eyelidUp.anchoredPosition != _targetPosition)
            {
                _newPosition = Vector2.Lerp(_eyelidUp.anchoredPosition, _targetPosition, _blinkSpeed * Time.deltaTime);
                _eyelidUp.anchoredPosition = _newPosition;
                _eyelidDown.anchoredPosition = -_newPosition;
            }
        }

        private void OnEnable() => SceneManager.sceneLoaded += SceneLoaded;

        private void OnDisable() => SceneManager.sceneLoaded += SceneLoaded;

        public void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_needToOpenEyes)
            {
                OpenEyelids();
            }
        }
    }
}
