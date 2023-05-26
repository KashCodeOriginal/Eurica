using System.Collections;
using Unit.GravityCube;
using UnityEngine;

namespace Unit.GraviTransformer
{
    public class GraviTransformerController : MonoBehaviour
    {
        [SerializeField] private GraviTransformerInput _leftInput;
        [SerializeField] private GraviTransformerInput _rightInput;
        [SerializeField] private GameObject _scalableCubePrefab;
        [SerializeField] private Transform _outputPosition;

        [SerializeField] private Animator _doorLeftAnim;
        [SerializeField] private Animator _doorRightAnim;
        [SerializeField] private bool _isLeftDoorOpen = true;

        private GravityCubeLogic _currentCube;

        private void OnEnable()
        {
            _leftInput.OnCubeInside += OnCubeInsideLeft;
            _rightInput.OnCubeInside += OnCubeInsideRight;
        }

        private void OnDisable()
        {
            _leftInput.OnCubeInside -= OnCubeInsideLeft;
            _rightInput.OnCubeInside -= OnCubeInsideRight;
        }

        private void OnCubeInsideLeft(bool inside, GravityCubeLogic cube)
        {
            if (inside)
                _currentCube = cube;
            else
                _currentCube = null;
        }

        public void PullLever()
        {
            if (_isLeftDoorOpen && _currentCube)
            {
                StartCoroutine(DoorAnimation(_currentCube, false));
            }
        }

        private void OnCubeInsideRight(bool inside, GravityCubeLogic cube)
        {
            if (!inside)
            {
                StartCoroutine(DoorAnimation(null, true));
            }
        }

        private IEnumerator DoorAnimation(GravityCubeLogic cube, bool isLeftOpening)
        {
            _doorLeftAnim.SetBool("isOpen", isLeftOpening);

            yield return new WaitForSeconds(3);

            _doorRightAnim.SetBool("isOpen", !isLeftOpening);

            if (cube)
            {
                Destroy(cube.gameObject);
                _currentCube = null;
                Instantiate(_scalableCubePrefab, _outputPosition.position, _outputPosition.rotation);
            }

            _isLeftDoorOpen = isLeftOpening;
        }
    }
}
