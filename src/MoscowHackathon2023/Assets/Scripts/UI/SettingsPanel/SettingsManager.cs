using Cinemachine;
using Data.StaticData.PlayerData;
using Services.Containers;
using Services.Input;
using Services.PlaySounds;
using TMPro;
using Unit.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace UI.SettingsPanel
{
    public class SettingsManager : MonoBehaviour
    {
        [Inject]
        public void Construct(PlayerInputActionReader playerInputActionReader,
            IGameInstancesContainer gameInstancesContainer,
            IPlaySoundsService playSoundsService)
        {
            _playerInputActionReader = playerInputActionReader;
            _gameInstancesContainer = gameInstancesContainer;
            _playSoundsService = playSoundsService;
        }

        public UnityAction<bool> OnChangePanelState;
    
        [Header("Settings Panel")]
        [SerializeField] private GameObject _settingsPanel;
        private PlayerInputActionReader _playerInputActionReader;
        private bool _isPanelOpened = false;

        private IGameInstancesContainer _gameInstancesContainer;
        private IPlaySoundsService _playSoundsService;

        [Header("Settings Data")]
        [SerializeField] private GameplaySettings _gameplaySettings;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private TextMeshProUGUI _volumeOutput;
        [SerializeField] private Slider _mouseSensSlider;
        [SerializeField] private TextMeshProUGUI _mouseSensOutput;

        [SerializeField] private Button _applyButton;
    
        private void Awake()
        {
            _settingsPanel.SetActive(false);
        }

        private void Start()
        {
            ApplyCurrentSettings();
        }

        private void OnEnable()
        {
            _playerInputActionReader.IsPlayerEscClicked += EscClicked;

            _volumeSlider.value = _gameplaySettings.SoundVolume;
            _mouseSensSlider.value = _gameplaySettings.MouseSens;
            
            _applyButton.onClick.AddListener(ApplyCurrentSettings);
        }

        private void OnDisable()
        {
            _playerInputActionReader.IsPlayerEscClicked -= EscClicked;
            
            _applyButton.onClick.AddListener(ApplyCurrentSettings);
        }

        private void Update()
        {
            _gameplaySettings.SoundVolume = _volumeSlider.value;
            _gameplaySettings.MouseSens = _mouseSensSlider.value;

            _volumeOutput.text = Mathf.RoundToInt(_volumeSlider.value * 100) + "%";
            _mouseSensOutput.text = _mouseSensSlider.value.ToString("0.0");
        }
        
        private void ApplyCurrentSettings()
        {
            _playSoundsService.SetUpVolumeMultiplier(_gameplaySettings.SoundVolume);
            
            var cinemachineComponent = _gameInstancesContainer.Player.GetComponent<PlayerChildContainer>()
                .CinemachineVirtualCamera. GetCinemachineComponent<CinemachinePOV>();

            cinemachineComponent.m_HorizontalAxis.m_MaxSpeed = _gameplaySettings.MouseSens / 100f;
            cinemachineComponent.m_VerticalAxis.m_MaxSpeed = _gameplaySettings.MouseSens / 100f;

        }

        private void EscClicked()
        {
            _isPanelOpened = !_isPanelOpened;
            UpdatePanelState();
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
                return;
            }
            
            _gameInstancesContainer.Player.GetComponent<PlayerMovement>().enabled = true;
            _gameInstancesContainer.Player.GetComponent<PlayerChildContainer>().CinemachineInputProvider.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
