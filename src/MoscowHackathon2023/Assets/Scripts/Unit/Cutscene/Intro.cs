using Unit.TriggerSystem;
using UnityEngine;

namespace Unit.Cutscene
{
    public class Intro : MonoBehaviour
    {
        [SerializeField] private TriggerTaskHelper _triggerTaskHelper;
        [SerializeField] private string _sceneName;
        
        public void OnEnable()
        {
            _triggerTaskHelper.ChangeScene(_sceneName);
        }
    }
}
