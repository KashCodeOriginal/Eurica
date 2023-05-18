using UnityEngine;

namespace Unit.Portal 
{ 
    public class PortalGun
    {
        private PortalFactory _portalFactory;

        public PortalGun(PortalFactory portalFactory) => _portalFactory = portalFactory;

        public void Fire(PortalType portalType) 
        { 
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));//Center the screen in the crosshairs

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.layer != 10)
                {
                    return; //10 - Wall                                                                
                }
            }
            
            _portalFactory.CreatePortal(hit.point, hit.normal, portalType); 
        }
    }
}

