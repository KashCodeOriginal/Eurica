using UnityEngine;

namespace Unit.Scene4.Task1
{
    [SelectionBase]
    public class QuizLampToggle : MonoBehaviour
    {
        [SerializeField] private QuizLampLogic _quizLampLogic;
        [Tooltip("0 set red, 1 set green, -1 ignore")]
        [SerializeField] private int[] _changes = new int[6];
        private bool overrideChanges = false;

        public void Press()
        {
            _quizLampLogic.SetChanges(_changes);
        }

        public void SetOverrideStatus(bool status)
        {
            overrideChanges = status;
        }

        private void Update()
        {
            if (overrideChanges)
                _quizLampLogic.SetChanges(_changes);
        }
    }
}