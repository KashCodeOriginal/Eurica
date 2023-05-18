using System.Threading.Tasks;
using Unit.GravityGun;
using Unit.MountRemote;
using Unit.Portal;
using Unit.ScaleGun;
using Unit.WeaponInventory;
using UnityEngine;

namespace Services.Factories.GunsFactory
{
    public interface IGunFactory
    {
        public Task<PortalGun> CreatePortalGun();
        public Task<GravityGun> CreateGravityGun();
        public Task<ScaleGun> CreateScaleGun();
        public Task<MountRemote> CreateMountRemove();
        public void Construct(Transform playerPickPlaceInHand);
    }
}