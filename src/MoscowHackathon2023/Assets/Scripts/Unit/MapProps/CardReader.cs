using System.Collections;
using System.Collections.Generic;
using Data.AssetsAddressablesConstants;
using Data.StaticData.BlinkSystem;
using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using UI.GameplayScreen;
using Unit.Base;
using Unit.Cutscene;
using UnityEngine;
using Zenject;

namespace Unit.MapProps
{
    public class CardReader : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _secondCutScene;
        [SerializeField] private MeshRenderer _indicatorMesh;
        [SerializeField] private Material _indicatorNotWorking;
        [SerializeField] private Material _indicatorWorking;
        [SerializeField] private MeshRenderer _displayMesh;
        [SerializeField] private Material _displayNotWorking;
        [SerializeField] private Material _displayWorking;
        private bool _working = false;

        public void Interact()
        {
            if (_working)
            {
                GameplayScreen.Instance.GameplayHintView.RequestHidingHint();
                GameplayScreen.Instance.GameplayTaskView.RequestHidingTask();
                
                _secondCutScene.SetActive(true);
                _secondCutScene.GetComponent<SecondCutScene>().CinemachineVirtualCamera.Priority = 12;
            }
        }

        public void SetWorkingStatus(bool working)
        {
            _working = working;
            _indicatorMesh.material = working ? _indicatorWorking : _indicatorNotWorking;
            _displayMesh.material = working ? _displayWorking : _displayNotWorking;
        }
    }
}
