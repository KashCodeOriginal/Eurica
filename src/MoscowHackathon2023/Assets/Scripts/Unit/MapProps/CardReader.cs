using System.Collections;
using System.Collections.Generic;
using Unit.Base;
using UnityEngine;

namespace Unit.MapProps
{
    public class CardReader : MonoBehaviour, IInteractable
    {
        [SerializeField] private MeshRenderer _mesh;
        [SerializeField] private Material _notWorkingMat;
        [SerializeField] private Material _workingMat;
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
            _mesh.material = working ? _workingMat : _notWorkingMat;
        }
    }
}
