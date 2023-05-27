using Services.Containers;
using Services.Input;
using Unit.ScaleGun;
using UnityEngine;
using Zenject;

namespace Unit.Training
{
    public class SecondHubTraining : MonoBehaviour
    {
        [Inject]
        public void Construct(PlayerInputActionReader playerInputActionReader, 
            IGameInstancesContainer gameInstancesContainer)
        {
            _playerInputActionReader = playerInputActionReader;
            _gameInstancesContainer = gameInstancesContainer;
        }

        private void OnEnable()
        {
            _playerInputActionReader.IsLeftButtonClicked += PlaySuccessDialog;
        }

        private PlayerInputActionReader _playerInputActionReader;
        private IGameInstancesContainer _gameInstancesContainer;

        [SerializeField] private GameObject _successDialog;

        private bool _isCompleted;

        private void PlaySuccessDialog()
        {
            if (gameObject.activeSelf == false)
            {
                return;
            }

            if (_isCompleted)
            {
                return;
            }
            
            var ray = _gameInstancesContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            
            if (!Physics.Raycast(ray, out var hit))
            {
                return;
            }

            if (hit.collider.TryGetComponent(out Scalable _))
            {
                _isCompleted = true;
                _successDialog.SetActive(true);
            }
        }
        
        private void OnDisable()
        {
            _playerInputActionReader.IsLeftButtonClicked -= PlaySuccessDialog;
        }
    }
}