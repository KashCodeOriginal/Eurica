using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplayScreen : MonoBehaviour
    {
        public static GameplayScreen Instance;
        
        public GameplayTaskView GameplayTaskView;
        public GameplayHintView GameplayHintView;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}