<<<<<<< Updated upstream
﻿using UnityEngine;
=======
﻿using Services.Input;
using System.Timers;
using Unit.Weapon;
using UnityEngine;
>>>>>>> Stashed changes

namespace Unit.Portal 
{ 
    public class PortalGun
    {
        private PortalFactory _portalFactory;

        public PortalGun(PortalFactory portalFactory) => _portalFactory = portalFactory;

<<<<<<< Updated upstream
        public void Fire(PortalType portalType) 
=======
        public void MainFire() 
            => Fire(PortalType.Blue);
        public void AlternateFire() 
            => Fire(PortalType.Red);

        private void PickUp() 
        {         
            _playerInputActionReader.IsRightButtonClicked += MainFire;
            _playerInputActionReader.IsLeftButtonClicked += AlternateFire;
        }

        private void Release() 
        { 
            _playerInputActionReader.IsRightButtonClicked -= MainFire;
            _playerInputActionReader.IsLeftButtonClicked -= AlternateFire;
        }

        private void Fire(PortalType portalType) 
>>>>>>> Stashed changes
        { 
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));//Center the screen in the crosshairs

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.layer != 10)
                {
<<<<<<< Updated upstream
                    return; //10 - Wall                                                                
                }
            }
            
            _portalFactory.CreatePortal(hit.point, hit.normal, portalType); 
=======
                    _portalFactory.CreatePortal(hit.point, hit.normal, portalType);
                    _isDelayFire = true;
                    _timer.Start(); 
                }                 
            }     
        }

        private void SetDelayFireStatus(System.Object source, ElapsedEventArgs e) 
        {            
            _isDelayFire = false;
            _timer.Stop();
>>>>>>> Stashed changes
        }
    }
}

