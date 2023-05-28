using UI.GameplayScreen;
using Unit.TriggerSystem;
using UnityEngine;

namespace Unit.Final
{
    public class Final : MonoBehaviour
    {
        [SerializeField] private TriggerTaskHelper _triggerTaskHelper;
        [SerializeField] private string _voiceMessage;

        public void PlayFinal()
        {
            _triggerTaskHelper.StartVoiceMessage(_voiceMessage);
            
            GameplayScreen.Instance.CanvasCredits.SetActive(true);
        }
    }
}
