using UnityEngine;

namespace Unit.UniversalGun
{
    public class WeaponSway : MonoBehaviour
    {
        [SerializeField] private float _swayAmount = 0.02f;
        [SerializeField] private float _swaySmoothness = 4.0f; 

        private Quaternion initialRotation;
        private Transform weaponTransform;
        private Transform cameraTransform;
        private Vector3 lastCameraPosition;

        private void Start()
        {
            weaponTransform = transform;
            initialRotation = weaponTransform.localRotation;
            cameraTransform = transform.parent.parent;
            lastCameraPosition = cameraTransform.position;
        }

        private void Update()
        {
            Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;

            float swayX = -cameraDelta.x * _swayAmount;
            float swayY = -cameraDelta.y * _swayAmount;

            Quaternion targetRotation = Quaternion.Euler(swayY, swayX, 0f) * initialRotation;
            weaponTransform.localRotation = Quaternion.Slerp(weaponTransform.localRotation, targetRotation, Time.deltaTime * _swaySmoothness);

            lastCameraPosition = cameraTransform.position;
        }
    }
}