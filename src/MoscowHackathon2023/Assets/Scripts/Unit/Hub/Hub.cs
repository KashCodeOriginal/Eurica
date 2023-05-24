using System;
using Services.GameProgress;
using Unit.Table;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Unit.Hub
{
    public class Hub : MonoBehaviour
    {
        [Inject]
        public void Construct(IGameProgressService gameProgressService)
        {
            _gameProgressService = gameProgressService;

            switch (_gameProgressService.CurrentLiftStage)
            {
                case LiftStage.First:
                    _firstLiftCutScene.SetActive(true);
                    
                    //_gameProgressService.SetUpLiftStage(LiftStage.Second);
                    break;
                case LiftStage.Second:
                    _firstLiftCutScene.SetActive(false);
                    _secondLiftCutScene.SetActive(true);
                    
                    //_gameProgressService.SetUpLiftStage(LiftStage.Third);
                    break;
                case LiftStage.Third:
                    _secondLiftCutScene.SetActive(false);
                    _thirdLiftCutScene.SetActive(true);
                    break;
            }

            switch (_gameProgressService.CurrentHubStage)
            {
                case HubStage.First:
                    _firstHubVariant.SetActive(true);
                    
                    //_gameProgressService.SetUpHubStage(HubStage.Second);
                    break;
                case HubStage.Second:
                    _firstHubVariant.SetActive(false);
                    _secondHubVariant.SetActive(true);
                    
                    //_gameProgressService.SetUpHubStage(HubStage.Third);
                    break;
                case HubStage.Third:
                    _secondHubVariant.SetActive(false);
                    _thirdHubVariant.SetActive(true);
                    
                    break;
            }
        }

        private void Start()
        {
            _tableOfIdeasWithGravityGun.IsGravityGunCreated +=
                () => _dialogWithMountAfterCollectingGravityGunTrigger.SetActive(true);
            
            _tableOfIdeasWithScaleGun.IsScaleGunCreated +=
                () =>
                {
                    _dialogAfterCollectingScaleGunTrigger.SetActive(true);
                    _openDoorSecondCutScene.SetActive(true);
                    _closeDoorSecondCutScene.SetActive(true);
                    _nextSceneTransition.SetActive(true);
                };
        }

        [SerializeField] private GameObject _firstLiftCutScene;
        [SerializeField] private GameObject _secondLiftCutScene;
        [SerializeField] private GameObject _thirdLiftCutScene;
        
        [SerializeField] private GameObject _firstHubVariant;
        [SerializeField] private GameObject _secondHubVariant;
        [SerializeField] private GameObject _thirdHubVariant;

        [SerializeField] private TableOfIdeas _tableOfIdeasWithGravityGun;
        [SerializeField] private GameObject _dialogWithMountAfterCollectingGravityGunTrigger;
        
        [SerializeField] private TableOfIdeas _tableOfIdeasWithScaleGun;
        [SerializeField] private GameObject _dialogAfterCollectingScaleGunTrigger;
        
        [SerializeField] private GameObject _openDoorSecondCutScene;
        [SerializeField] private GameObject _closeDoorSecondCutScene;
        [SerializeField] private GameObject _nextSceneTransition;

        private IGameProgressService _gameProgressService;
    }
}