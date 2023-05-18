using Infrastructure;
using Infrastructure.ProjectStateMachine.Base;
using Services.Factories;
using Unit.GravityGun;
using Unit.Portal;
using Unit.ScaleGun;
using UnityEngine;

namespace Tools
{
    public class LoadTestState : IState<BootstrapTest>, IEnterable, IExitable
    {
        public BootstrapTest Initializer { get; }

        private readonly GunFactory _gunFactory;
        private PortalGun _portalGun;
        private GravityGun _gravityGun;
        private ScaleGun _scaleGun;

        public LoadTestState(GunFactory gunFactory)
        {            
            _gunFactory = gunFactory;
        }

        public async void OnEnter()
        {
            _portalGun = await _gunFactory.CreatePortalGun();
            _gravityGun = await _gunFactory.CreateGravityGun();
            _scaleGun = await _gunFactory.CreateScaleGun();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OnExit()
        {
            
        }
    }
}