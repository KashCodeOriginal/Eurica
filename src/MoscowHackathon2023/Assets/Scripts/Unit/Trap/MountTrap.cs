using System;
using Unit.SceneControllers.Scene2.Task1;
using Unit.Table;
using Unit.UniversalGun;
using UnityEngine;

namespace Unit.Trap
{
    public class MountTrap : MonoBehaviour
    {
        [SerializeField] private QuizLampLogic _quizLampLogic;
        [SerializeField] private Animator _animator;
        [SerializeField] private TableOfIdeas _tableOfIdeas;
        
        private static readonly int TrapGoUp = Animator.StringToHash("TrapGoUp");

        private void Start()
        {
            _quizLampLogic.OnQuizCompleted += MountTrapUp;
        }

        private void MountTrapUp()
        {
            _animator.SetTrigger(TrapGoUp);
            
            _tableOfIdeas.SetUpType(GunTypes.Gravity);
        }

        private void OnDisable()
        {
            _quizLampLogic.OnQuizCompleted -= MountTrapUp;
        }
    }
}
