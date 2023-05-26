using Data.StaticData.BlinkSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenuScreen
{
    public class MainMenuScreenController : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;

        public event Action OnPlayButtonClicked;

        private void Start()
        {
            _startGameButton.onClick.AddListener(PlayButtonPressed);
        }

        private void PlayButtonPressed()
        {
            StartCoroutine(PlayButtonPressedDelay());
        }

        private IEnumerator PlayButtonPressedDelay()
        {
            if (BlinkSystem.Instance)
            {
                BlinkSystem.Instance.CloseEyelids();
                yield return new WaitForSeconds(BlinkSystem.Instance.GetPauseTime);
            }

            OnPlayButtonClicked?.Invoke();
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(PlayButtonPressed);
        }
    }
}