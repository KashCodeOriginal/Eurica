using UI.GameplayScreen;
using Unit.TriggerSystem;
using UnityEngine;

namespace Unit.Cutscene
{
    public class IntroStart : MonoBehaviour
    {
        public void Start()
        {
            GameplayScreen.Instance?.SetVisibilityOfPlayerUI(false);
            
            //var cinemachineBrain = FindObjectOfType<>()
        }
    }
}
