using System;
using UnityEngine;

namespace Unit.UniversalGun
{
    public class UniversalGunView : MonoBehaviour
    {
        [field: SerializeField] public Transform GravityAttachPoint { get; private set; }
        [field: SerializeField] public Transform GuineaPigAttachPoint { get; private set; }
        
        [SerializeField] private GameObject _portalGun;
        [SerializeField] private GameObject _gravityGun;
        [SerializeField] private GameObject _scaleGun;
        [SerializeField] private GameObject _mountGun;

        private GameObject _currentGun;
        
        private static readonly int EmissiveExposureWeight = Shader.PropertyToID("_EmissiveExposureWeight");

        private void Start()
        {
            _currentGun = _portalGun;
        }

        public void ChangeActiveGun(GunTypes gunType)
        {
            ChangeCurrentGunEmission(1);

            SetNewGun(gunType);
            
            ChangeCurrentGunEmission(0);
        }

        private void ChangeCurrentGunEmission(float value)
        {
            _currentGun.GetComponent<MeshRenderer>().material.SetFloat(EmissiveExposureWeight, value);
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
                    _currentGun = _mountGun;
                    break;
            }
        }
    }
}