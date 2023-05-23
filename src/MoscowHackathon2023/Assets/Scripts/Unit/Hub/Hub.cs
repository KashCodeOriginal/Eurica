using Services.GameProgress;
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
                    
                    _gameProgressService.SetUpLiftStage(LiftStage.Second);
                    break;
                case LiftStage.Second:
                    _firstLiftCutScene.SetActive(false);
                    _secondLiftCutScene.SetActive(true);
                    
                    _gameProgressService.SetUpLiftStage(LiftStage.Third);
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
                    
                    _gameProgressService.SetUpHubStage(HubStage.Second);
                    break;
                case HubStage.Second:
                    _firstHubVariant.SetActive(false);
                    _secondHubVariant.SetActive(true);
                    
                    _gameProgressService.SetUpHubStage(HubStage.Third);
                    break;
                case HubStage.Third:
                    _secondHubVariant.SetActive(false);
                    _thirdHubVariant.SetActive(true);
                    
                    break;
            }
        }

        [SerializeField] private GameObject _firstLiftCutScene;
        [SerializeField] private GameObject _secondLiftCutScene;
        [SerializeField] private GameObject _thirdLiftCutScene;
        
        [SerializeField] private GameObject _firstHubVariant;
        [SerializeField] private GameObject _secondHubVariant;
        [SerializeField] private GameObject _thirdHubVariant;

        private IGameProgressService _gameProgressService;
    }
}