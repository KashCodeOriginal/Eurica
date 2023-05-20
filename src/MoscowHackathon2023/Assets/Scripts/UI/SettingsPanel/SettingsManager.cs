using Services.Input;
using UnityEngine;
using Zenject;

public class SettingsManager : MonoBehaviour
{
    [Inject]
    public void Construct(PlayerInputActionReader playerInputActionReader)
    {
        _playerInputActionReader = playerInputActionReader;
    }

    private PlayerInputActionReader _playerInputActionReader;
    private bool _isPanelOpened = false;
    [SerializeField] private GameObject _settingsPanel;

    private void Awake()
    {
        _settingsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        _playerInputActionReader.IsPlayerEscClicked += EscClicked;
    }

    private void OnDisable()
    {
        _playerInputActionReader.IsPlayerEscClicked -= EscClicked;
    }

    private void EscClicked()
    {
        _isPanelOpened = !_isPanelOpened;
        _settingsPanel.SetActive(_isPanelOpened);
    }
}
