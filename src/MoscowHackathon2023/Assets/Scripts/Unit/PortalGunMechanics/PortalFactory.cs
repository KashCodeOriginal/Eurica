using Data.AssetsAddressablesConstants;
using Services.Factories.AbstractFactory;
using UnityEngine;
using Zenject;

namespace PortalGunMechanics { 
    public class PortalFactory
    {
        private IAbstractFactory _abstractFactory;
        private DiContainer _container;
        public Portal Portal { get; private set; }

        public PortalFactory(DiContainer container, IAbstractFactory abstractFactory)
        {
            _abstractFactory = abstractFactory;
            _container = container;
        }

        public PortalGun CreatePortalGun()
        {
            return new PortalGun(this);
        }

        public async void CreatePortal(Vector3 position, Vector3 face, PortalType portalView)
        {
            var portalInstance =
                await _abstractFactory.CreateInstance<Portal>(AssetsAddressablesConstants.PORTAL_PREFAB);

            portalInstance.transform.rotation = Quaternion.LookRotation(face);
            portalInstance.transform.position = position + portalInstance.transform.forward * 0.6f;

            SetUp(portalInstance, portalView);
        }

        private void SetUp(Portal portalInstance,  PortalType portalView)
        {
            portalInstance.Construct(portalView);
        }
    }

    public enum PortalType { 
        Blue,
        Red
    }
}

