using Data.StaticData.BlinkSystem;
using Data.StaticData.VoicePhrases;
using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using Services.PlaySounds;
using Services.StaticData;
using System.Collections;
using Services.Containers;
using UI.GameplayScreen;
using Unit.UniversalGun;
using UnityEngine;
using Zenject;

namespace Unit.TriggerSystem
{
    public class TriggerTaskHelper : MonoBehaviour
    {
        [Inject]
        public void Construct(Bootstrap bootstrap,
            IStaticDataService staticDataService,
            IPlaySoundsService playSoundsService,
            IGameInstancesContainer gameInstancesContainer)
        {
            _bootstrap = bootstrap;
            _staticDataService = staticDataService;
            _playSoundsService = playSoundsService;
            _gameInstancesContainer = gameInstancesContainer;
        }

        private Bootstrap _bootstrap;
        private IStaticDataService _staticDataService;
        private IPlaySoundsService _playSoundsService;
        private IGameInstancesContainer _gameInstancesContainer;

        public void ShowHint(string hint)
        {
            GameplayScreen.Instance?.GameplayHintView.RequestShowingHint(hint);
        }

        public void HideHint()
        {
            GameplayScreen.Instance?.GameplayHintView.RequestHidingHint();
        }

        public void ShowTask(string task)
        {
            GameplayScreen.Instance?.GameplayTaskView.RequestShowingTask(task);
        }

        public void HideTask()
        {
            GameplayScreen.Instance?.GameplayTaskView.RequestHidingTask();
        }

        public void FailTask()
        {
            GameplayScreen.Instance?.GameplayTaskView.RequestTaskFail();
        }

        public void StartVoiceMessage(string audioID)
        {
            VoiceMessage voiceMessage = _staticDataService.GetVoiceMessageByID(audioID);

            if (voiceMessage == null)
            {
                Debug.LogError("No sound by id " + audioID);
            }
            else
            {
                if (_playSoundsService.CanPlay(voiceMessage.AudioClip, canPlayMultiple: false, playOnlyOnce: true))
                {
                    GameplayScreen.Instance?.GameplaySubtitlesView.ShowSubtitles(voiceMessage);
                    _playSoundsService.PlayAudioClip(voiceMessage.AudioClip, VolumeLevel.VoiceOver);
                }
            }
        }

        public void BlinkAndOpen() => BlinkSystem.Instance?.BlinkAndOpen();

        public void OpenEyelids() => BlinkSystem.Instance?.OpenEyelids();

        public void OpenEyelidsAfterTime(float time)
        {
            StartCoroutine(OpenAfterTime(time));
        }

        private IEnumerator OpenAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            BlinkSystem.Instance?.OpenEyelids();
        }

        public void CloseEyelids() => BlinkSystem.Instance?.CloseEyelids();

        public void CheatSpeed(float speed) => Time.timeScale = speed;

        public void CheatGuns()
        {
            var universalGunView = _gameInstancesContainer.UniversalGunView;
            
            _gameInstancesContainer.TurnOnWeapon();
            
            _gameInstancesContainer.Inventory.Weapons[0].SetUpUniversalView(universalGunView);
            _gameInstancesContainer.AddViewGun(GunTypes.Portal);
            universalGunView.PortalGunBody.SetActive(true);
            
            _gameInstancesContainer.Inventory.Weapons[1].SetUpUniversalView(universalGunView);
            _gameInstancesContainer.AddViewGun(GunTypes.Gravity);
            
            _gameInstancesContainer.Inventory.Weapons[2].SetUpUniversalView(universalGunView);
                    
            _gameInstancesContainer.AddViewGun(GunTypes.Scale);
                    
            universalGunView.ScaleGunBody.SetActive(true);
        }

        public void ChangeScene(string sceneName)
        {
            StartCoroutine(ChangeSceneAfterBlink(sceneName));
        }

        private IEnumerator ChangeSceneAfterBlink(string sceneName)
        {
            if (BlinkSystem.Instance)
            {
                BlinkSystem.Instance.CloseEyelids();
                yield return new WaitForSeconds(BlinkSystem.Instance.GetPauseTime);
            }
            GameplayScreen.Instance?.ResetHintsTasks();
            _bootstrap.StateMachine.SwitchState<GameLoadingState, string>(sceneName);
        }
    }
}
