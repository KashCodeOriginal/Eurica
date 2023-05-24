using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.SceneControllers.Scene4
{
    public class QuizLampLogic : MonoBehaviour
    {
        public UnityEvent OnQuizCompleted;
        public UnityEvent OnDoorOpened;

        private int[] _progress;
        [SerializeField] private MeshRenderer[] _indicators;
        [SerializeField] private Material _indicatorOn;
        [SerializeField] private Material _indicatorOff;

        [SerializeField] private Transform _secretDoor;
        [SerializeField] private Vector3 _openedLocalPos;
        [SerializeField] private float _openingTime = 5f;

        private bool _isCompletedAlready = false;

        private void Awake()
        {
            _progress = new int[6];
            foreach (var indicator in _indicators)
            {
                indicator.material = _indicatorOff;
            }
        }

        /// <param name="changes">0 set red, 1 set green, -1 ignore</param>
        public void SetChanges(int[] changes)
        {
            for (int i = 0; i < changes.Length; i++)
            {
                if (changes[i] != -1)
                {
                    _progress[i] = changes[i];
                    _indicators[i].material = _progress[i] == 0 ? _indicatorOff : _indicatorOn;
                }
            }

            if (IsCompleted() && !_isCompletedAlready)
            {
                _isCompletedAlready = true;
                OnQuizCompleted?.Invoke();

                StartCoroutine(OpenDoor());
            }
        }

        private IEnumerator OpenDoor()
        {
            float elapsedTime = 0;

            while (elapsedTime < _openingTime)
            {
                _secretDoor.localPosition = Vector3.Lerp(Vector3.zero, _openedLocalPos, elapsedTime / _openingTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _secretDoor.localPosition = _openedLocalPos;
            OnDoorOpened?.Invoke();
        }

        private bool IsCompleted()
        {
            bool allGreen = true;

            foreach (var progress in _progress)
            {
                if (progress == 0)
                {
                    allGreen = false;
                }
            }
            return allGreen;
        }
    }
}