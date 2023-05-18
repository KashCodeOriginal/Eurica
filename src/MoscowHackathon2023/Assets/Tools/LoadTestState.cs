using Infrastructure.ProjectStateMachine.Base;
using Unit;
using Unit.GravityGunMechanics;
using Unit.Portal;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class LoadTestState : IState<BootstrapTest>, IEnterable, IExitable
    {
        public BootstrapTest Initializer { get; }

        private readonly GunFactory _gunFactory;
        private PortalGun _portalGun;
        private GravityGun _gravityGun;

        public LoadTestState(
            GunFactory gunFactory)
        {            
            _gunFactory = gunFactory;
        }

        public async void OnEnter()
        {          
            
            _portalGun = await _gunFactory.CreatePortalGun();
            _gravityGun = await _gunFactory.CreateGravityGun();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnExit()
        {
            
        }
    }
}