using System;
using UnityEngine;

namespace Unit.TriggerSystem
{
    public class DoorTrigger : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _blackWall;
        
        private static readonly int OpenLiftDoor = Animator.StringToHash("OpenLiftDoor");
        private static readonly int CloseLiftDoor = Animator.StringToHash("CloseLiftDoor");
        

        public void OpenDoors()
        {
            _animator.SetTrigger(OpenLiftDoor);
        }

        public void CloseDoors()
        {
            _animator.SetTrigger(CloseLiftDoor);

            if (_blackWall == null)
            {
                return;
            }
            
            _blackWall.SetActive(false);
        }
    }
}
