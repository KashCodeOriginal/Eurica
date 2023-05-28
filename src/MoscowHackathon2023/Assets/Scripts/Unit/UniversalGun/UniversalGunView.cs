using System;
using Services.GameProgress;
using Services.PlaySounds;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

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

        [SerializeField] private Image _currentWeaponIcon;

        [SerializeField] private Sprite _portalIcon;
        [SerializeField] private Sprite _scaleIcon;
        [SerializeField] private Sprite _gravityIcon;

        [SerializeField] private AudioClip _portalAudioClip;
        [SerializeField] private AudioClip _scaleAudioClip;
        [SerializeField] private AudioClip _gravityAudioClip;

        private IGameProgressService _gameProgressService;
        private IPlaySoundsService _playSoundsService;

        public void Construct(IGameProgressService gameProgressService, IPlaySoundsService playSoundsService)
        {
            _gameProgressService = gameProgressService;
            _playSoundsService = playSoundsService;
        }

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
            _gameProgressService.SetUpCurrentWeapon(gunType);

            switch (gunType)
            {
                case GunTypes.Portal:
                    _currentGun = _portalGun;
                    _currentWeaponIcon.sprite = _portalIcon;
                    _playSoundsService.PlayAudioClip(_portalAudioClip, 0.3f, true, false, 0.95f, 1.05f);
                    break;
                case GunTypes.Gravity:
                    _currentGun = _gravityGun;
                    _currentWeaponIcon.sprite = _gravityIcon;
                    _playSoundsService.PlayAudioClip(_gravityAudioClip, 0.3f, true, false, 0.95f, 1.05f);
                    break;
                case GunTypes.Scale:
                    _currentGun = _scaleGun;
                    _currentWeaponIcon.sprite = _scaleIcon;
                    _playSoundsService.PlayAudioClip(_scaleAudioClip, 0.3f, true, false, 0.95f, 1.05f);
                    break;
            }
        }
    }
}