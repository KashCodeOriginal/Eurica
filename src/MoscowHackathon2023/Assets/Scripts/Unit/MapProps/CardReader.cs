using System.Collections;
using System.Collections.Generic;
using Data.AssetsAddressablesConstants;
using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using UI.GameplayScreen;
using Unit.Base;
using UnityEngine;
using Zenject;

namespace Unit.MapProps
{
    public class CardReader : MonoBehaviour, IInteractable
    {
        [SerializeField] private MeshRenderer _indicatorMesh;
        [SerializeField] private Material _indicatorNotWorking;
        [SerializeField] private Material _indicatorWorking;
        [SerializeField] private MeshRenderer _displayMesh;
        [SerializeField] private Material _displayNotWorking;
        [SerializeField] private Material _displayWorking;
        private bool _working = false;

        private Bootstrap _bootstrap;

        [Inject]
        public void Construct(Bootstrap bootstrap)
        {
            _bootstrap = bootstrap;
        }

        public void Interact()
        {
            if (_working)
            {
                GameplayScreen.Instance.GameplayHintView.RequestHidingHint();
                GameplayScreen.Instance.GameplayTaskView.RequestHidingTask();

                // TODO: Start 2nd cutscene for the 1st level.

                _bootstrap.StateMachine.SwitchState<GameLoadingState, string>(AssetsAddressablesConstants.SCENE2_MAIN_HUB);
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
