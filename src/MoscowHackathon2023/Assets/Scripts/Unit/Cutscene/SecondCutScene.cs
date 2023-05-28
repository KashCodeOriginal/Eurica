using System;
using System.Collections;
using Cinemachine;
using Data.AssetsAddressablesConstants;
using Data.StaticData.BlinkSystem;
using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using Services.Containers;
using UnityEngine;
using Zenject;

namespace Unit.Cutscene
{
    public class SecondCutScene : MonoBehaviour
    {
        [Inject]
        public void Construct(Bootstrap bootstrap, IGameInstancesContainer gameInstancesContainer)
        {
            _bootstrap = bootstrap;
            _gameInstancesContainer = gameInstancesContainer;
        }

        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        private Bootstrap _bootstrap;
        private IGameInstancesContainer _gameInstancesContainer;

        public CinemachineVirtualCamera CinemachineVirtualCamera => _cinemachineVirtualCamera;

        public void Start()
        {
            //_gameInstancesContainer.TurnOffPlayerIU();
            _gameInstancesContainer.TurnOffPlayer();
        }

        public void OnCutSceneEnded()
        {
            StartCoroutine(ChangeSceneAfterBlink(AssetsAddressablesConstants.SCENE2_MAIN_HUB));
        }

        private IEnumerator ChangeSceneAfterBlink(string sceneName)
        {
            if (BlinkSystem.Instance)
            {
                BlinkSystem.Instance.CloseEyelids();
                //_gameInstancesContainer.TurnOnPlayerUI();
                yield return new WaitForSeconds(BlinkSystem.Instance.GetPauseTime);
            }
            _bootstrap.StateMachine.SwitchState<GameLoadingState, string>(sceneName);
        }
    }
}