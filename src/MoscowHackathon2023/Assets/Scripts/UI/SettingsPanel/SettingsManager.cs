using Data.StaticData.PlayerData;
using Services.Input;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class SettingsManager : MonoBehaviour
{
    [Inject]
    public void Construct(PlayerInputActionReader playerInputActionReader)
    {
        _playerInputActionReader = playerInputActionReader;
    }

    public UnityAction<bool> OnChangePanelState;
    
    [Header("Settings Panel")]
    [SerializeField] private GameObject _settingsPanel;
    private PlayerInputActionReader _playerInputActionReader;
    private bool _isPanelOpened = false;

    [Header("Settings Data")]
    [SerializeField] private GameplaySettings _gameplaySettings;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TextMeshProUGUI _volumeOutput;
    [SerializeField] private Slider _mouseSensSlider;
    [SerializeField] private TextMeshProUGUI _mouseSensOutput;
    
    private void Awake()
    {
        _settingsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        _playerInputActionReader.IsPlayerEscClicked += EscClicked;

        _volumeSlider.value = _gameplaySettings.SoundVolume;
        _mouseSensSlider.value = _gameplaySettings.MouseSens;
    }

    private void OnDisable()
    {
        _playerInputActionReader.IsPlayerEscClicked -= EscClicked;
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

    private void Update()
    {
        _gameplaySettings.SoundVolume = _volumeSlider.value;
        _gameplaySettings.MouseSens = _mouseSensSlider.value;

        _volumeOutput.text = Mathf.RoundToInt(_volumeSlider.value * 100) + "%";
        _mouseSensOutput.text = _mouseSensSlider.value.ToString("0.0");
    }

    private void UpdatePanelState()
    {
        _settingsPanel.SetActive(_isPanelOpened);
        OnChangePanelState?.Invoke(_isPanelOpened);
    }
}
