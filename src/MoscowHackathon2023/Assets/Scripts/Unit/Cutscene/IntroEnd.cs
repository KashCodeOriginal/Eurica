using UI.GameplayScreen;
using Unit.TriggerSystem;
using UnityEngine;

namespace Unit.Cutscene
{
    public class IntroEnd : MonoBehaviour
    {
        [SerializeField] private TriggerTaskHelper _triggerTaskHelper;
        [SerializeField] private string _sceneName;
        
        public void OnEnable()
        {
            GameplayScreen.Instance?.SetVisibilityOfPlayerUI(true);
            _triggerTaskHelper.ChangeScene(_sceneName);
        }
    }
}
