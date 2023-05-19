using Infrastructure;
using Infrastructure.ProjectStateMachine.States;
<<<<<<< Updated upstream
=======
using Services.PlaySounds;
using Services.StaticData;
>>>>>>> Stashed changes
using UI.GameplayScreen;
using UnityEngine;
using Zenject;

namespace Unit.TriggerSystem
{
    public class TriggerTaskHelper : MonoBehaviour
    {
        [Inject]
<<<<<<< Updated upstream
        public void Construct(Bootstrap bootstrap)
=======
        public void Construct(Bootstrap bootstrap, 
            IStaticDataService staticDataService,
            IPlaySoundsService playSoundsService)
>>>>>>> Stashed changes
        {
            _bootstrap = bootstrap;
            _staticDataService = staticDataService;
            _playSoundsService = playSoundsService;
        }

        private Bootstrap _bootstrap;
<<<<<<< Updated upstream
=======
        private IStaticDataService _staticDataService;
        private IPlaySoundsService _playSoundsService;
>>>>>>> Stashed changes

        public void ShowHint(string hint)
        {
            GameplayScreen.Instance.GameplayHintView.RequestShowingHint(hint);
        }

        public void HideHint()
        {
            GameplayScreen.Instance.GameplayHintView.RequestHidingHint();
        }

        public void ShowTask(string task)
        {
            GameplayScreen.Instance.GameplayTaskView.RequestShowingTask(task);
        }

        public void FailTask()
        {
            GameplayScreen.Instance.GameplayTaskView.RequestTaskFail();
        }

        public void StartVoiceMessage(string audioID)
        {
<<<<<<< Updated upstream
            // TODO: Start monologue with audio file and subtitles in a custom system.
=======
            var voiceMessage = _staticDataService.GetVoiceMessageByID(audioID);
            
            _playSoundsService.PlayOneShot(voiceMessage.AudioClip, 1f);
>>>>>>> Stashed changes
        }

        public void ChangeScene(string sceneName)
        {
            _bootstrap.StateMachine.SwitchState<GameLoadingState, string>(sceneName);
        }
    }
}
