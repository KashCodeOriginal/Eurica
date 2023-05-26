using System.Collections;
using Unit.GravityCube;
using UnityEngine;

namespace Unit.GraviTransformer
{
    public class GraviTransformerController : MonoBehaviour
    {
        [SerializeField] private GraviTransformerInput _input;
        [SerializeField] private GameObject _scalableCubePrefab;
        [SerializeField] private Transform _outputPosition;

        [SerializeField] private Animator _doorLeftAnim;
        [SerializeField] private Animator _doorRightAnim;
        [SerializeField] private bool _isLeftDoorOpen = true;

        private void OnEnable()
        {
            _input.OnCubeInside += OnCubeInside;
        }

        private void OnDisable()
        {
            _input.OnCubeInside -= OnCubeInside;
        }

        private void OnCubeInside(GravityCubeLogic cube)
        {
            if (_isLeftDoorOpen)
            {
                StartCoroutine(DoorOpenCloseAnimation(cube));
            }
        }

        private IEnumerator DoorOpenCloseAnimation(GravityCubeLogic cube)
        {
            _isLeftDoorOpen = false;
            _doorLeftAnim.SetBool("isOpen", false);

            yield return new WaitForSeconds(1);

            _doorRightAnim.SetBool("isOpen", true);

            Destroy(cube.gameObject);
            Instantiate(_scalableCubePrefab, _outputPosition.position, _outputPosition.rotation);
        }
    }
}
