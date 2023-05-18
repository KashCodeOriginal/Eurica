using PortalMechanics;
using Services.Input;
using System.Timers;
using UnityEngine;

namespace Unit.Portal 
{ 
    public class PortalGun : IWeaponed
    {
        private PortalFactory _portalFactory;
        private readonly PortalGunView _portalGunView;
        private readonly PlayerInputActionReader _playerInputActionReader;
        private Timer _timer;
        private bool _isDelayFire = false;        
        private LayerMask layerMask = LayerMask.NameToLayer("Walls");

        public PortalGun(PortalFactory portalFactory, PortalGunView portalGunView, PlayerInputActionReader playerInputActionReader)
        {
            _portalFactory = portalFactory;
            _portalGunView = portalGunView;
            _playerInputActionReader = playerInputActionReader;
            _timer = new Timer(1000);            
            _timer.Elapsed += SetDelayFireStatus;
            _portalGunView.PickedUp.AddListener(PickUp);
            _portalGunView.Released.AddListener(Release);
        }

        public void MainFire() 
            => Fire(PortalType.Blue);
        public void AlternateFire() 
            => Fire(PortalType.Red);

        private void PickUp() {         
            _playerInputActionReader.IsRightButtonClicked += MainFire;
            _playerInputActionReader.IsLeftButtonClicked += AlternateFire;
        }

        private void Release() { 
            _playerInputActionReader.IsRightButtonClicked -= MainFire;
            _playerInputActionReader.IsLeftButtonClicked -= AlternateFire;
        }

        private void Fire(PortalType portalType) 
        { 
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));//Center the screen in the crosshairs

            if (Physics.Raycast(ray, out var hit) && _isDelayFire == false)
            {
                if (hit.collider.gameObject.layer == layerMask)
                {
                    _portalFactory.CreatePortal(hit.point, hit.normal, portalType);
                    _isDelayFire = true;
                    _timer.Start(); 
                }                 
            }     
        }

        private void SetDelayFireStatus(System.Object source, System.Timers.ElapsedEventArgs e) {            
            _isDelayFire = false;
            _timer.Stop();
        }
    }
}

