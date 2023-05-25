using Cinemachine;
using Data.StaticData.PlayerData;
using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using Services.Containers;
using Services.Input;
using Services.PlaySounds;
using TMPro;
using Unit.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI.SettingsPanel
{
    public class SettingsManager : MonoBehaviour
    {
        [Inject]
        public void Construct(PlayerInputActionReader playerInputActionReader,
            IGameInstancesContainer gameInstancesContainer,
            IPlaySoundsService playSoundsService,
            Bootstrap bootstrap)
        {
            _playerInputActionReader = playerInputActionReader;
            _gameInstancesContainer = gameInstancesContainer;
            _playSoundsService = playSoundsService;
            _bootstrap = bootstrap;
        }

        public UnityAction<bool> OnChangePanelState;

        [Header("Settings Panel")]
        [SerializeField] private GameObject _settingsPanel;
        private PlayerInputActionReader _playerInputActionReader;
        private bool _isPanelOpened = false;

        private IGameInstancesContainer _gameInstancesContainer;
        private IPlaySoundsService _playSoundsService;
        private Bootstrap _bootstrap;

        [Header("Settings Data")]
        [SerializeField] private GameplaySettings _gameplaySettings;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private TextMeshProUGUI _volumeOutput;
        [SerializeField] private Slider _mouseSensSlider;
        [SerializeField] private TextMeshProUGUI _mouseSensOutput;
        [SerializeField] private GameObject _subtitlesCheckmark;

        private void OnEnable()
        {
            InitValues();

            _playerInputActionReader.IsPlayerEscClicked += EscClicked;

            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            _mouseSensSlider.onValueChanged.AddListener(OnMouseSensChanged);
            _subtitlesCheckmark.SetActive(_gameplaySettings.Subtitles);

            _settingsPanel.SetActive(false);
        }

        private void InitValues()
        {
            _volumeSlider.value = _gameplaySettings.SoundVolume;
            _mouseSensSlider.value = _gameplaySettings.MouseSens;
            ApplyCurrentSettings();
        }

        private void OnDisable()
        {
            _playerInputActionReader.IsPlayerEscClicked -= EscClicked;

            _mouseSensSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            _volumeSlider.onValueChanged.RemoveListener(OnMouseSensChanged);
        }

        private void Update()
        {
            _gameplaySettings.SoundVolume = _volumeSlider.value;
            _gameplaySettings.MouseSens = _mouseSensSlider.value;

            _volumeOutput.text = Mathf.RoundToInt(_volumeSlider.value * 100) + "%";
            _mouseSensOutput.text = ((int)_mouseSensSlider.value).ToString();
        }

        private void OnVolumeChanged(float value)
        {
            _gameplaySettings.SoundVolume = value;
            ApplyCurrentSettings();
        }

        private void OnMouseSensChanged(float value)
        {
            _gameplaySettings.MouseSens = value;
            ApplyCurrentSettings();
        }

        public void ClickSubtitles()
        {
            _gameplaySettings.Subtitles = !_gameplaySettings.Subtitles;
            _subtitlesCheckmark.SetActive(_gameplaySettings.Subtitles);
            ApplyCurrentSettings();
        }

        private void ApplyCurrentSettings()
        {
            _playSoundsService.SetUpVolumeMultiplier(_gameplaySettings.SoundVolume);

            var cinemachineComponent = _gameInstancesContainer.Player.GetComponent<PlayerChildContainer>()
                .CinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>();

            GameplayScreen.GameplayScreen.Instance?.GameplaySubtitlesView.SetSettings(_gameplaySettings.Subtitles);

            cinemachineComponent.m_HorizontalAxis.m_MaxSpeed = _gameplaySettings.MouseSens / 100f;
            cinemachineComponent.m_VerticalAxis.m_MaxSpeed = _gameplaySettings.MouseSens / 100f;
        }

        private void EscClicked()
        {
            _isPanelOpened = !_isPanelOpened;
            UpdatePanelState();
        }

        public void RestartLevel()
        {
            ClosePanel();

            var currentScene = SceneManager.GetActiveScene();

            _playSoundsService.ResetSoundStates();
            GameplayScreen.GameplayScreen.Instance?.GameplaySubtitlesView.HideSubtitles();

            _bootstrap.StateMachine.SwitchState<GameLoadingState, string>(currentScene.name);
        }

        public void ClosePanel()
        {
            _isPanelOpened = false;
            UpdatePanelState();
        }

        private void UpdatePanelState()
        {
            _settingsPanel.SetActive(_isPanelOpened);
            OnChangePanelState?.Invoke(_isPanelOpened);

            if (_isPanelOpened)
            {
                _gameInstancesContainer.Player.GetComponent<PlayerMovement>().enabled = false;
                _gameInstancesContainer.Player.GetComponent<PlayerChildContainer>().CinemachineInputProvider.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                _gameInstancesContainer.Player.GetComponent<PlayerMovement>().enabled = true;
                _gameInstancesContainer.Player.GetComponent<PlayerChildContainer>().CinemachineInputProvider.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
