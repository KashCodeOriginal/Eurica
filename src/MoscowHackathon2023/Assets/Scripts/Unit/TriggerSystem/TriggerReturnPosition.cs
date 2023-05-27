using UnityEngine;

namespace Unit.TriggerSystem
{
    public class TriggerReturnPosition : MonoBehaviour
    {
        [SerializeField] private Transform _returnPoint;

        public Transform ReturnPoint => _returnPoint;
    }
}
