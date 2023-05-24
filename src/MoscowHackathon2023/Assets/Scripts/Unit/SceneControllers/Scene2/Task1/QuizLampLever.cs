using UnityEngine;

namespace Unit.SceneControllers.Scene2.Task1
{
    public class QuizLampLever : MonoBehaviour
    {
        [SerializeField] private QuizLampLogic _quizLampLogic;
        [Tooltip("0 set red, 1 set green, -1 ignore")]
        [SerializeField] private int[] _changes = new int[5];

        public void Press()
        {
            _quizLampLogic.SetChanges(_changes);
        }
    }
}