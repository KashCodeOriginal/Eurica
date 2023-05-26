using System;
using Services.Input;
using Unit.TriggerSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Unit.Training
{
    public class FirstHubTraining : MonoBehaviour
    {
        [SerializeField] private GameObject _leftMouseHintTrigger;
        [SerializeField] private GameObject _leftMouseReleaseHintTrigger;
        [SerializeField] private GameObject _scrollHintTrigger;
        [SerializeField] private TriggerTaskHelper _triggerTaskHelper;

        private PlayerInputActionReader _playerInputActionReader;

        private bool _isCubeTaken = false;
        
        private bool _allCompleted = false;
        
        [Inject]
        public void Construct(PlayerInputActionReader playerInputActionReader)
        {
            _playerInputActionReader = playerInputActionReader;
        }

        private void OnEnable()
        {
            _playerInputActionReader.IsLeftButtonClicked += TurnOffLeftHint;

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

        private void TurnOffLeftHint()
        {
            if (gameObject.activeSelf == false)
            {
                return;
            }

            if (_allCompleted)
            {
                _leftMouseReleaseHintTrigger.SetActive(false);
                _triggerTaskHelper.HideHint();  
            }

            if (!_isCubeTaken)
            {
                _leftMouseHintTrigger.SetActive(false);
                
                _leftMouseReleaseHintTrigger.SetActive(true);

                _isCubeTaken = true;
                
                _allCompleted = true;
            }
        }

        private void OnDisable()
        {
            _playerInputActionReader.IsLeftButtonClicked -= TurnOffLeftHint;

            _playerInputActionReader.IsMouseScroll -= TurnOffScrollHint;
        }
    }
}
