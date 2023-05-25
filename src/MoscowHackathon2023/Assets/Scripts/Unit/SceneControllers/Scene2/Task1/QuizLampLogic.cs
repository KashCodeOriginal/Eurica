using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Unit.SceneControllers.Scene2.Task1
{
    public class QuizLampLogic : MonoBehaviour
    {
        public UnityAction OnQuizCompleted;
        
        private int[] _progress;
        [SerializeField] private MeshRenderer[] _indicators;
        [SerializeField] private Material _indicatorOn;
        [SerializeField] private Material _indicatorOff;

        [SerializeField] private bool _canUse = false;

        private void Awake()
        {
            _progress = new int[5];
        }

        /// <param name="changes">0 set red, 1 set green, -1 ignore</param>
        public void SetChanges(int[] changes)
        {
            if (!_canUse)
            {
                return;
            }
            
            for (int i = 0; i < changes.Length; i++)
            {
                if (changes[i] != -1)
                {
                    _progress[i] = changes[i];
                    _indicators[i].material = _progress[i] == 0 ? _indicatorOff : _indicatorOn;
                }
            }

            if (!IsCompleted())
            {
                return;
            }
            
            OnQuizCompleted?.Invoke();
            _canUse = false;
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

        public void SetCanUse(bool condition)
        {
            _canUse = condition;
        }
    }
}