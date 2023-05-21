using Services.Containers;
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

        public void OnCutSceneEnded()
        {
            _gameInstancesContainer.TurnOnPlayer();
        }
    }
}
