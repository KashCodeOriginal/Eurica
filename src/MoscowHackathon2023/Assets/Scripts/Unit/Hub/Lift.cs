using Unit.TriggerSystem;
using UnityEngine;

namespace Unit.Hub
{
    public class Lift : MonoBehaviour
    {
        [SerializeField] private SoundHelper _openDoors;
        [SerializeField] private bool _playSoundOnStart = true;

        private void Start()
        {
            if (_playSoundOnStart)
                _openDoors.PlaySound(0.1f);
        }
    }
}