using System;
using Services.GameProgress;
using Unit.Base;
using UnityEngine;
using Zenject;

namespace Unit.Key
{
    public class Key : MonoBehaviour, IInteractable
    {
        [Inject]
        public void Construct(IGameProgressService gameProgressService)
        {
            _gameProgressService = gameProgressService;
        }

        private IGameProgressService _gameProgressService;

        [SerializeField] private GameObject _firstHubOpenDoorTrigger;
        [SerializeField] private GameObject _firstHubCloseDoorTrigger;
        [SerializeField] private GameObject _firstDialogToExitTrigger;

        public void Interact()
        {
            switch (_gameProgressService.CurrentHubStage)
            {
                case HubStage.First:
                    _firstHubOpenDoorTrigger.SetActive(true);
                    _firstHubCloseDoorTrigger.SetActive(true);
                    _firstDialogToExitTrigger.SetActive(true);
                    break;
                case HubStage.Second:
                    break;
                case HubStage.Third:
                    break;
            }
        }
    }
}
