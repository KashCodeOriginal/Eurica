using UnityEngine;

namespace UI.GameplayScreen
{
    public class GameplayScreen : MonoBehaviour
    {
        public static GameplayScreen Instance;
        
        public GameplayTaskView GameplayTaskView;
        public GameplayHintView GameplayHintView;
        public GameplaySubtitlesView GameplaySubtitlesView;

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
    }
}