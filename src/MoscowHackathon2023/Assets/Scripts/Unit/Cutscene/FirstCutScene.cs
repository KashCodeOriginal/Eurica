using System;
using Services.Containers;
using UI.GameplayScreen;
using UnityEngine;
using Zenject;

namespace Unit.Cutscene
{
    public class FirstCutScene : MonoBehaviour
    {
        [Inject]
        public void Construct(IGameInstancesContainer gameInstancesContainer)
        {
            _gameInstancesContainer = gameInstancesContainer;
        }

        private IGameInstancesContainer _gameInstancesContainer;

        private void Start()
        {
            GameplayScreen.Instance?.SetVisibilityOfPlayerUI(false);
        }

        public void OnCutSceneEnded()
        {
            _gameInstancesContainer.TurnOnPlayer();
            GameplayScreen.Instance?.SetVisibilityOfPlayerUI(true);
        }
    }
}
