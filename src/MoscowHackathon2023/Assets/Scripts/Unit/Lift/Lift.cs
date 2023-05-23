using System;
using Services.GameProgress;
using UnityEngine;
using Zenject;

namespace Unit.Lift
{
    public class Lift : MonoBehaviour
    {
        [Inject]
        public void Construct(IGameProgressService gameProgressService)
        {
            _gameProgressService = gameProgressService;

            switch (_gameProgressService.CurrentLiftStage)
            {
                case LiftStage.First:
                    _firstLiftCutScene.SetActive(true);
                    
                    _gameProgressService.SetUpLiftStage(LiftStage.Second);
                    
                    break;
                case LiftStage.Second:
                    _secondLiftCutScene.SetActive(true);
                    
                    _gameProgressService.SetUpLiftStage(LiftStage.Third);
                    
                    break;
                case LiftStage.Third:
                    _thirdLiftCutScene.SetActive(true);
                    break;
            }
        }

        [SerializeField] private GameObject _firstLiftCutScene;
        [SerializeField] private GameObject _secondLiftCutScene;
        [SerializeField] private GameObject _thirdLiftCutScene;

        private IGameProgressService _gameProgressService;
    }
}