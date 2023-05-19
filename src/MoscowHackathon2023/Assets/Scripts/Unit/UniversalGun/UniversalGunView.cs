using UnityEngine;

namespace Unit.UniversalGun
{
    public class UniversalGunView : MonoBehaviour
    {
        [SerializeField] private Transform _gravityAttachPoint;

        public Transform GravityAttachPoint => _gravityAttachPoint;
    }
}