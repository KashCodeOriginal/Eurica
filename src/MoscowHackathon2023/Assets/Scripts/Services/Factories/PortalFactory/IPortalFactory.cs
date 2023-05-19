using Unit.Portal;
using UnityEngine;

namespace Services.Factories.PortalFactory
{
    public interface IPortalFactory
    {
        public void CreatePortal(Vector3 position, Vector3 face, PortalType portalView);
    }
}