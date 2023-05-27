using Services.Containers;
using Services.Input;
using Unit.ScaleGun;
using Unit.TriggerSystem;
using UnityEngine;
using Zenject;

namespace Unit.Training
{
    public class ThirdHubTraining : MonoBehaviour
    {
        [Inject]
        public void Construct(PlayerInputActionReader playerInputActionReader)
        {
            _playerInputActionReader = playerInputActionReader;
        }

        private void OnEnable()
        {
            _playerInputActionReader.IsLeftButtonClicked += HideLeftHint;
            _playerInputActionReader.IsRightButtonClicked += HideRightHint;
        }

        private PlayerInputActionReader _playerInputActionReader;

        [SerializeField] private GameObject _leftMouseHint;

        [SerializeField] private GameObject _rightMouseHint;

        [SerializeField] private TriggerTaskHelper _triggerTaskHelper;

        private void HideLeftHint()
        {
            _leftMouseHint.SetActive(false);
            _rightMouseHint.SetActive(true);
        }

        private void HideRightHint()
        {
            _rightMouseHint.SetActive(false);
            
            _triggerTaskHelper.HideHint();
        }

        private void OnDisable()
        {
            _playerInputActionReader.IsLeftButtonClicked -= HideLeftHint;
            _playerInputActionReader.IsRightButtonClicked -= HideRightHint;
        }
    }
}
