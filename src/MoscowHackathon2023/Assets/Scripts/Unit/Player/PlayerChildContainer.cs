using UnityEngine;

namespace Unit.Player
{
    public class PlayerChildContainer : MonoBehaviour
    {
        [SerializeField] private Transform _headTransform;

        public Transform HeadTransform => _headTransform;   
    }
}
