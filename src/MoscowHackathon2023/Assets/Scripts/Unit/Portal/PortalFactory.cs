using System.Collections.Generic;
using Data.AssetsAddressablesConstants;
using Services.Factories.AbstractFactory;
using System.Threading.Tasks;
using UnityEngine;
using Services.Input;

namespace Unit.Portal { 
    public class PortalFactory
    {
        private IAbstractFactory _abstractFactory;        
        private Dictionary<PortalType, Portal> _pullPortals = new Dictionary<PortalType, Portal>();        

        public Portal Portal { get; private set; }

        public PortalFactory(IAbstractFactory abstractFactory)
        {
            _abstractFactory = abstractFactory;            
        }

        public async void CreatePortal(Vector3 position, Vector3 face, PortalType portalView)
        {
            PortalType typeOppositePortal = portalView == PortalType.Red ? PortalType.Blue : PortalType.Red;
            Portal createdPortal = null;
            Portal oppositePortal = null;

            if (_pullPortals.ContainsKey(portalView)) 
            { 
                createdPortal = _pullPortals[portalView]; 
            }

            if (_pullPortals.ContainsKey(typeOppositePortal)) 
            { 
                oppositePortal = _pullPortals[typeOppositePortal]; 
            }

            if (createdPortal == null) 
            {                
                var createdPortalGO = await _abstractFactory.CreateInstance<GameObject>(AssetsAddressablesConstants.PORTAL_PREFAB);
                createdPortal = createdPortalGO.GetComponent<Portal>();
            }  
            createdPortal.transform.rotation = Quaternion.LookRotation(face);            
            createdPortal.transform.position = position + createdPortal.transform.forward * 0.6f;
            SetUpPortal(createdPortal, portalView, oppositePortal);

        }

        private void SetUpPortal(Portal portalInstance,  PortalType portalView, Portal oppositePortal)
        {
            portalInstance.Construct(oppositePortal);
            _pullPortals[portalView] = portalInstance; 
        }
    }
}

