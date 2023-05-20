using Services.Input;
using TMPro;
using Unity.VisualScripting;
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
    public UnityAction<bool> OnChangeSettingsData;
    
    [Header("Settings Panel")]
    [SerializeField] private GameObject _settingsPanel;
    private PlayerInputActionReader _playerInputActionReader;
    private bool _isPanelOpened = false;

    [Header("Settings Data")]
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

        _volumeSlider.value = PlayerPrefs.GetFloat("GameVolume", 0.5f);
        _mouseSensSlider.value = PlayerPrefs.GetFloat("MouseSensivity", 8f);
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
        PlayerPrefs.SetFloat("GameVolume", _volumeSlider.value);
        PlayerPrefs.SetFloat("MouseSensivity", _mouseSensSlider.value);

        _volumeOutput.text = Mathf.RoundToInt(_volumeSlider.value * 100) + "%";
        _mouseSensOutput.text = _mouseSensSlider.value.ToString("0.0");

        // TODO: Invoke OnSettingsData updated.
    }

    private void UpdatePanelState()
    {
        _settingsPanel.SetActive(_isPanelOpened);
        OnChangePanelState?.Invoke(_isPanelOpened);
    }
}
