using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenuScreen
{
    public class MainMenuScreenController : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _testSceneButton;

        public event Action OnPlayButtonClicked;
        public event Action OnTestButtonClicked;

        private void Start()
        {
            _startGameButton.onClick.AddListener(PlayButtonPressed);
            _testSceneButton.onClick.AddListener(TestButtonPressed);
        }

        private void PlayButtonPressed()
        {
            OnPlayButtonClicked?.Invoke();
        }

        private void TestButtonPressed()
        {
            OnTestButtonClicked?.Invoke();
        }
    
        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(PlayButtonPressed);
            _testSceneButton.onClick.RemoveListener(TestButtonPressed);
        }
    }
}