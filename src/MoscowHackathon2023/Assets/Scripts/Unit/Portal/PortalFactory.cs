using Data.AssetsAddressablesConstants;
using Services.Factories.AbstractFactory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PortalMechanics { 
    public class PortalFactory
    {
        private IAbstractFactory _abstractFactory;        
        private Dictionary<PortalType, Portal> _pullPortals = new Dictionary<PortalType, Portal>();
        public Portal Portal { get; private set; }

        public PortalFactory(IAbstractFactory abstractFactory)
        {
            _abstractFactory = abstractFactory;
        }

        public PortalGun CreatePortalGun()
        {
            return new PortalGun(this);
        }

        public async void CreatePortal(Vector3 position, Vector3 face, PortalType portalView)
        {
            PortalType typeOppositePortal = portalView == PortalType.Red ? PortalType.Blue : PortalType.Red;
            Portal createdPortal = _pullPortals[portalView];            
            Portal oppositePortal = _pullPortals[typeOppositePortal];

            if (createdPortal == null)
                createdPortal = await _abstractFactory.CreateInstance<Portal>(AssetsAddressablesConstants.PORTAL_PREFAB);                
                          
            createdPortal.transform.rotation = Quaternion.LookRotation(face);
            createdPortal.transform.position = position + createdPortal.transform.forward * 0.6f;
            SetUp(createdPortal, portalView, oppositePortal);
        }

        private void SetUp(Portal portalInstance, PortalType portalView, Portal oppositePortal)
        {
            portalInstance.Construct(oppositePortal);
            _pullPortals[portalView] = portalInstance; 
        }
    }

    public enum PortalType { 
        Blue,
        Red
    }
}

