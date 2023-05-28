using UI.SettingsPanel;
using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplayScreen : MonoBehaviour
    {
        public static GameplayScreen Instance;
        
        public GameplayTaskView GameplayTaskView;
        public GameplayHintView GameplayHintView;
        public GameplaySubtitlesView GameplaySubtitlesView;
        public SettingsManager SettingsManager;

        [SerializeField] private Transform _inventoryTransform;
        [SerializeField] private GameObject _staticCanvas;

        public Transform InventoryTransform => _inventoryTransform;
        public GameObject StaticCanvas => _staticCanvas;

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

        public void SetVisibilityOfPlayerUI(bool isVisible)
        {
            GameplayTaskView.GetComponent<CanvasGroup>().alpha = isVisible ? 1 : 0;
            GameplayHintView.GetComponent<CanvasGroup>().alpha = isVisible ? 1 : 0;
            _staticCanvas.GetComponent<CanvasGroup>().alpha = isVisible ? 1 : 0;
        }

        public void ResetHintsTasks()
        {
            GameplayHintView?.RequestHidingHint();
            GameplayTaskView?.RequestHidingTask();
        }
    }
}