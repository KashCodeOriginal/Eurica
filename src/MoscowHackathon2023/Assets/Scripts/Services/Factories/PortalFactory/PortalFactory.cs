using System.Collections.Generic;
using Data.AssetsAddressablesConstants;
using Services.Containers;
using Services.Factories.AbstractFactory;
using Unit.Portal;
using UnityEngine;

namespace Services.Factories.PortalFactory 
{
    public class PortalFactory : IPortalFactory
    {
        private IAbstractFactory _abstractFactory;
        private readonly IGameInstancesContainer _gameInstancesContainer;

        private Dictionary<PortalType, Portal> _pullPortals = new();        

        public Portal Portal { get; private set; }
        
        private LayerMask _portalLayer = LayerMask.NameToLayer("Portal");

        public PortalFactory(IAbstractFactory abstractFactory, IGameInstancesContainer gameInstancesContainer)
        {
            _abstractFactory = abstractFactory;
            _gameInstancesContainer = gameInstancesContainer;
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
            
            SetUpPortal(createdPortal, portalView, oppositePortal, _gameInstancesContainer.Player.transform);
        }

        private void SetUpPortal(Portal portalInstance,  PortalType portalView, Portal oppositePortal, Transform playerTransform)
        {
            portalInstance.Construct(oppositePortal, playerTransform, portalView);
            _pullPortals[portalView] = portalInstance; 
        }
    }
}

