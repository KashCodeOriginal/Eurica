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

        [SerializeField] private GraviTransformerIndicator[] indicators;

        private GravityCubeLogic _currentCube;

        private void OnEnable()
        {
            _leftInput.OnCubeInside += OnCubeInsideLeft;
            _rightInput.OnCubeInside += OnCubeInsideRight;

            UpdateIndicators(false, false);
        }

        private void OnDisable()
        {
            _leftInput.OnCubeInside -= OnCubeInsideLeft;
            _rightInput.OnCubeInside -= OnCubeInsideRight;
        }

        private void OnCubeInsideLeft(bool inside, GravityCubeLogic cube)
        {
            UpdateIndicators(inside, false);

            if (inside)
                _currentCube = cube;
            else
                _currentCube = null;
        }

        public void PullLever()
        {
            if (_isLeftDoorOpen && _currentCube)
            {
                StartCoroutine(DoorAnimationCloseLeft(_currentCube));
            }
        }

        private void OnCubeInsideRight(bool inside, GravityCubeLogic cube)
        {
            if (!inside)
            {
                StartCoroutine(DoorAnimationOpenLeft());
            }
        }

        private IEnumerator DoorAnimationCloseLeft(GravityCubeLogic cube)
        {
            _doorLeftAnim.SetBool("isOpen", false);
            UpdateIndicators(false, false);

            yield return new WaitForSeconds(3);
 
            _doorRightAnim.SetBool("isOpen", true);

            Destroy(cube.gameObject);
            Instantiate(_scalableCubePrefab, _outputPosition.position, _outputPosition.rotation);

            UpdateIndicators(false, true);
            _isLeftDoorOpen = false;
        }


        private IEnumerator DoorAnimationOpenLeft()
        {
            UpdateIndicators(false, false);
            _doorLeftAnim.SetBool("isOpen", true);
            _doorRightAnim.SetBool("isOpen", false);

            yield return new WaitForSeconds(1);

            _isLeftDoorOpen = true;
        }

        private void UpdateIndicators(bool glowLeft, bool glowRight)
        {
            foreach (var indicator in indicators)
            {
                indicator.SetStatus(glowLeft, glowRight);
            }
        }
    }
}
