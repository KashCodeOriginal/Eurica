using Services.GameProgress;
using Unit.Table;
using UnityEngine;
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
                    
                    break;
                case LiftStage.Second:
                    _firstLiftCutScene.SetActive(false);
                    _secondLiftCutScene.SetActive(true);
                    
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
                    
                    break;
                case HubStage.Second:
                    _firstHubVariant.SetActive(false);
                    _secondHubVariant.SetActive(true);
                    
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
                () =>
                {
                    _firstScreenTraining.SetActive(true);
                    
                    _dialogWithMountAfterCollectingGravityGunTrigger.SetActive(true);
                    _scrollHintTrigger.SetActive(true);
                    
                    _keyGetTask.SetActive(true);
                };
            
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

        [SerializeField] private GameObject _firstScreenTraining;
        [SerializeField] private GameObject _scrollHintTrigger;

        [SerializeField] private GameObject _keyGetTask;
        
        [SerializeField] private TableOfIdeas _tableOfIdeasWithScaleGun;
        [SerializeField] private GameObject _dialogAfterCollectingScaleGunTrigger;
        
        [SerializeField] private GameObject _openDoorSecondCutScene;
        [SerializeField] private GameObject _closeDoorSecondCutScene;
        [SerializeField] private GameObject _nextSceneTransition;

        private IGameProgressService _gameProgressService;
    }
}