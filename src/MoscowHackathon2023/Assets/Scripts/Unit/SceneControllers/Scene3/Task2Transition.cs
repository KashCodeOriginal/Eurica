using System.Collections;
using UnityEngine;

namespace SceneControllers.Scene3
{
    public class Task2Transition : MonoBehaviour
    {
        [SerializeField] private Scene3Task2 _scene3Task2;
        [SerializeField] private Transform _ceiling;
        [SerializeField] private Vector3 _ceilingTargetStage2;
        [SerializeField] private float _transitionTime = 8f;
        [SerializeField] private GameObject _objectsStage2;

        private void Start()
        {
            _scene3Task2.OnTaskCompleted.AddListener(OnTaskCompleted);
        }

        private void OnDestroy()
        {
            _scene3Task2.OnTaskCompleted.RemoveListener(OnTaskCompleted);
        }

        private void OnTaskCompleted()
        {
            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            _objectsStage2.SetActive(true);

            float elapsedTime = 0f;

            while (elapsedTime < _transitionTime)
            {
                _ceiling.localPosition = Vector3.Lerp(Vector3.zero, _ceilingTargetStage2, elapsedTime / _transitionTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _ceiling.localPosition = _ceilingTargetStage2;
        }
    }
}