using System.Collections;
using System.Collections.Generic;
using Unit.Base;
using UnityEngine;

namespace Unit.MapProps
{
    public class CardReader : MonoBehaviour, IInteractable
    {
        [SerializeField] private MeshRenderer _indicatorMesh;
        [SerializeField] private Material _indicatorNotWorking;
        [SerializeField] private Material _indicatorWorking;
        [SerializeField] private MeshRenderer _displayMesh;
        [SerializeField] private Material _displayNotWorking;
        [SerializeField] private Material _displayWorking;
        private bool _working = false;

        public void Interact()
        {
            if (_working)
            {
                Debug.Log("<color=purple>LEVEL COMPLETED</color>");
            }
        }

        public void SetWorkingStatus(bool working)
        {
            _working = working;
            _indicatorMesh.material = working ? _indicatorWorking : _indicatorNotWorking;
            _displayMesh.material = working ? _displayWorking : _displayNotWorking;
        }
    }
}
