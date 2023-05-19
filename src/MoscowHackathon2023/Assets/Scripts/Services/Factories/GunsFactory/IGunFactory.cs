using System.Threading.Tasks;
using Unit.GravityGun;
using Unit.MountRemote;
using Unit.Portal;
using Unit.ScaleGun;
using Unit.UniversalGun;
using Unit.WeaponInventory;
using UnityEngine;

namespace Services.Factories.GunsFactory
{
    public interface IGunFactory
    {
        public void Construct(Transform playerPickPlaceInHand);
        public PortalGun CreatePortalGun();
        public GravityGun CreateGravityGun();
        public ScaleGun CreateScaleGun();
        public Task<MountRemote> CreateMountRemove();
        public Task<UniversalGunView> CreateUniversalGunView();
    }
}