using System.Collections.Generic;
using Data.AssetsAddressablesConstants;
using Services.Factories.AbstractFactory;
using UnityEngine;

namespace Unit.Portal 
{ 
    public class PortalFactory
    {
        private IAbstractFactory _abstractFactory;        
        
        private Dictionary<PortalType, Portal> _pullPortals = new();        

        public Portal Portal { get; private set; }
        
        private LayerMask _portalLayer = LayerMask.NameToLayer("Portal");

        public PortalFactory(IAbstractFactory abstractFactory)
        {
            _abstractFactory = abstractFactory;            
        }

        public async void CreatePortal(Vector3 position, Vector3 face, PortalType portalView)
        {
            PortalType typeOppositePortal = portalView == PortalType.Red ? PortalType.Blue : PortalType.Red;
            Portal createdPortal = null;
            Portal oppositePortal = null;

            if (_pullPortals.TryGetValue(portalView, out var portal)) 
            { 
                createdPortal = portal; 
            }

            if (_pullPortals.TryGetValue(typeOppositePortal, out var pullPortal)) 
            { 
                oppositePortal = pullPortal; 
            }

            if (createdPortal == null) 
            {                
                var createdPortalGO = await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PORTAL_PREFAB);

                createdPortalGO.layer = _portalLayer;
                
                createdPortal = createdPortalGO.GetComponent<Portal>();
            }  

            createdPortal.transform.position = position;
            createdPortal.transform.rotation = Quaternion.FromToRotation(Vector3.forward, face);
            
            SetUpPortal(createdPortal, portalView, oppositePortal);
        }

        private void SetUpPortal(Portal portalInstance,  PortalType portalView, Portal oppositePortal)
        {
            portalInstance.Construct(oppositePortal);
            _pullPortals[portalView] = portalInstance; 
        }
    }
}

