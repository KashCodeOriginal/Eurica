using UnityEngine;
using UnityEngine.Serialization;

namespace Unit.UniversalGun
{
    public class UniversalGunView : MonoBehaviour
    {
        [field: SerializeField] public Transform GravityAttachPoint { get; private set; }
        [field: SerializeField] public Transform GuineaPigAttachPoint { get; private set; }

        public GameObject ScaleGunBody => _scaleGunBody;
        public GameObject PortalGunBody => _portalGunBody;

        [SerializeField] private GameObject _portalGun;
        [SerializeField] private GameObject _gravityGun;
        [SerializeField] private GameObject _scaleGun;
        
        [SerializeField] private GameObject _currentGun;

        [SerializeField] private GameObject _scaleGunBody;
        [SerializeField] private GameObject _portalGunBody;

        public void ChangeActiveGun(GunTypes gunType)
        {
            ChangeCurrentGunEmission(1);

            SetNewGun(gunType);

            ChangeCurrentGunEmission(0);
        }

        private void ChangeCurrentGunEmission(float value)
        {
            _currentGun.GetComponent<UniversalGunLamp>().SetCurrentState(value);
        }

        private void SetNewGun(GunTypes gunType)
        {
            switch (gunType)
            {
                case GunTypes.Portal:
                    _currentGun = _portalGun;
                    break;
                case GunTypes.Gravity:
                    _currentGun = _gravityGun;
                    break;
                case GunTypes.Scale:
                    _currentGun = _scaleGun;
                    break;
                case GunTypes.Mount:
                    break;
            }
        }
    }
}