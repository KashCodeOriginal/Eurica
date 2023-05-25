using System;
using Services.Input;
using Unit.TriggerSystem;
using UnityEngine;
using Zenject;

namespace Unit.Training
{
    public class FirstHubTraining : MonoBehaviour
    {
        [SerializeField] private GameObject _leftMouseHintTrigger;
        [SerializeField] private GameObject _rightMouseHintTrigger;
        [SerializeField] private GameObject _scrollHintTrigger;
        [SerializeField] private TriggerTaskHelper _triggerTaskHelper;

        private PlayerInputActionReader _playerInputActionReader;
        
        [Inject]
        public void Construct(PlayerInputActionReader playerInputActionReader)
        {
            _playerInputActionReader = playerInputActionReader;

            _playerInputActionReader.IsLeftButtonClicked += TurnOffLeftHint;
            _playerInputActionReader.IsRightButtonClicked += TurnOffRightHint;

            _playerInputActionReader.IsMouseScroll += TurnOffScrollHint;
        }

        private void TurnOffScrollHint(float obj)
        {
            if (gameObject.activeSelf == false)
            {
                return;
            }
            
            _scrollHintTrigger.SetActive(false);
            
            _leftMouseHintTrigger.SetActive(true);
        }

        private void TurnOffRightHint()
        {
            if (gameObject.activeSelf == false)
            {
                return;
            }
            
            _rightMouseHintTrigger.SetActive(false);
            
            _triggerTaskHelper.HideHint();
            
            gameObject.SetActive(false);
        }

        private void TurnOffLeftHint()
        {
            if (gameObject.activeSelf == false)
            {
                return;
            }
            
            _leftMouseHintTrigger.SetActive(false);
            
            _rightMouseHintTrigger.SetActive(true);
        }

        private void OnDisable()
        {
            _playerInputActionReader.IsLeftButtonClicked -= TurnOffLeftHint;
            _playerInputActionReader.IsRightButtonClicked -= TurnOffRightHint;

            _playerInputActionReader.IsMouseScroll -= TurnOffScrollHint;
        }
    }
}
