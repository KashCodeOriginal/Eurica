using System.Collections;
using Data.StaticData.GunData;
using Data.StaticData.GunData.GravityGunData;
using Infrastructure;
using Services.Containers;
using Services.Input;
using Unit.GravityCube;
using Unit.UniversalGun;
using Unit.Weapon;
using UnityEngine;

namespace Unit.GravityGun
{
    public class GravityGun : IWeaponed
    {
        public BaseGunData GunData => _gravityGunGravityGunData;

        private Rigidbody _currentRigidbody;

        private ICoroutineRunner _coroutineRunner;

        private Coroutine _dragIn;

        private PlayerInputActionReader _playerInputActionReader;

        private readonly GravityGunData _gravityGunGravityGunData;

        private UniversalGunView _universalGunView;

        private readonly IGameInstancesContainer _gameInstancesContainer;

        private LayerMask _interactiveLayer = LayerMask.NameToLayer("InteractiveObjectForGravity");

        private LayerMask _grabbedLayer = LayerMask.NameToLayer("Grabbed");

        public void SetUpUniversalView(UniversalGunView universalGunView)
        {
            _universalGunView = universalGunView;
        }


        public GravityGun(ICoroutineRunner coroutineRunner,
            PlayerInputActionReader playerInputActionReader,
            GravityGunData gravityGunGravityGunData,
            IGameInstancesContainer gameInstancesContainer)
        {
            _coroutineRunner = coroutineRunner;
            _playerInputActionReader = playerInputActionReader;
            _gravityGunGravityGunData = gravityGunGravityGunData;
            _gameInstancesContainer = gameInstancesContainer;
        }

        public void MainFire()
        {
            var ray = _gameInstancesContainer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (!Physics.Raycast(ray, out var hit, _gravityGunGravityGunData.CatchDistance))
            {
                return;
            }

            if (hit.collider.gameObject.layer != _interactiveLayer)
            {
                return;
            }


            _currentRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
            _dragIn = _coroutineRunner.StartCoroutine(DragIn());

            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsLeftButtonClicked += StopMainFire;


            if (_currentRigidbody == null)
            {
                Debug.LogError("InteractiveObjectForGravity does not have a Rigidbody");
            }
            else
            {
                if (_currentRigidbody.gameObject.TryGetComponent(out GravityCubeLogic gravityCube))
                {
                    gravityCube.RequestDetach += OnRequestToDetach;
                }
            }

            _currentRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _currentRigidbody.gameObject.layer = _grabbedLayer;
        }

        private void StopMainFire()
        {
            _coroutineRunner.StopCoroutine(_dragIn);
            _playerInputActionReader.IsLeftButtonClicked -= StopMainFire;
            _playerInputActionReader.IsLeftButtonClicked += MainFire;

            if (_currentRigidbody != null)
            {
                if (_currentRigidbody.gameObject.TryGetComponent(out GravityCubeLogic gravityCube))
                {
                    gravityCube.RequestDetach -= OnRequestToDetach;
                }

                _currentRigidbody.constraints = RigidbodyConstraints.None;
                _currentRigidbody.gameObject.layer = _interactiveLayer;
                _currentRigidbody = null;
            }
        }

        private void OnRequestToDetach()
        {
            StopMainFire();
        }

        public void AlternateFire()
        {
            if (_currentRigidbody == null)
            {
                return;
            }

            _coroutineRunner.StopCoroutine(_dragIn);
            _currentRigidbody.velocity = _universalGunView.GravityAttachPoint.forward * _gravityGunGravityGunData.DropPower;

            _currentRigidbody.constraints = RigidbodyConstraints.None;
            _currentRigidbody.gameObject.layer = _interactiveLayer;

            float rAng = 15f;
            Quaternion randomRotation = Quaternion.Euler(Random.Range(-rAng, rAng), Random.Range(-rAng, rAng), Random.Range(-rAng, rAng));
            _currentRigidbody.AddTorque(randomRotation.eulerAngles, ForceMode.Impulse);

            if (_currentRigidbody.gameObject.TryGetComponent(out GravityCubeLogic gravityCube))
            {
                gravityCube.RequestDetach -= OnRequestToDetach;
            }

            _currentRigidbody = null;
        }

        public void Select()
        {
            _playerInputActionReader.IsLeftButtonClicked += MainFire;
            _playerInputActionReader.IsRightButtonClicked += AlternateFire;
            
            _universalGunView.ChangeActiveGun(GunTypes.Gravity);
        }

        public void Deselect()
        {
            if (_dragIn != null)
            {
                _coroutineRunner.StopCoroutine(_dragIn);
            }

            _playerInputActionReader.IsLeftButtonClicked -= StopMainFire;
            _playerInputActionReader.IsLeftButtonClicked -= MainFire;
            _playerInputActionReader.IsRightButtonClicked -= AlternateFire;
        }

        private IEnumerator DragIn()
        {
            while (true)
            {
                if (_currentRigidbody)
                {
                    _currentRigidbody.velocity =
                        (_universalGunView.GravityAttachPoint.position -
                         (_currentRigidbody.transform.position + _currentRigidbody.centerOfMass)) * _gravityGunGravityGunData.CatchPower;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
