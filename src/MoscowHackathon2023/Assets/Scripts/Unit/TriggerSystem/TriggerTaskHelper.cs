using Data.StaticData.VoicePhrases;
using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
using Services.PlaySounds;
using Services.StaticData;
using UI.GameplayScreen;
using UnityEngine;
using Zenject;

namespace Unit.TriggerSystem
{
    public class TriggerTaskHelper : MonoBehaviour
    {
        [Inject]
        public void Construct(Bootstrap bootstrap, 
            IStaticDataService staticDataService,
            IPlaySoundsService playSoundsService)
        {
            _bootstrap = bootstrap;
            _staticDataService = staticDataService;
            _playSoundsService = playSoundsService;
        }

        private Bootstrap _bootstrap;
        private IStaticDataService _staticDataService;
        private IPlaySoundsService _playSoundsService;

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
                return;
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

        public void ChangeScene(string sceneName)
        {
            _bootstrap.StateMachine.SwitchState<GameLoadingState, string>(sceneName);
        }
    }
}
