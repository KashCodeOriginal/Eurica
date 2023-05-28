using UnityEngine;

namespace Unit.SceneControllers.Scene4
{
    public class Scene4AnimEvent : MonoBehaviour
    {
        [SerializeField] private Scene4Ladder _scriptOnOtherObject;
        
        public void DoIt()
        {
            _scriptOnOtherObject.PlayScrapeSound();
        }
    }
}
