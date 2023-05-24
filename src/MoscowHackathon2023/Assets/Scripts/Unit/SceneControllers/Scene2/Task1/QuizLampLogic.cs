using UnityEngine;
using UnityEngine.Events;

namespace Unit.SceneControllers.Scene2.Task1
{
    public class QuizLampLogic : MonoBehaviour
    {
        public UnityAction OnQuizCompleted;
        
        private int[] _progress;
        [SerializeField] private MeshRenderer[] _indicators;
        [SerializeField] private Material _indicatorOn;
        [SerializeField] private Material _indicatorOff;

        private void Awake()
        {
            _progress = new int[5];
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

            if (IsCompleted())
            {
                OnQuizCompleted?.Invoke();
            }
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