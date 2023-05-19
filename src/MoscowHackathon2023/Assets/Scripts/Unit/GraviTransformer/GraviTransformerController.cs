using Unit.GravityCube;
using UnityEngine;

public class GraviTransformerController : MonoBehaviour
{
    [SerializeField] private GraviTransformerInput _input;
    [SerializeField] private GameObject _scalableCubePrefab;
    [SerializeField] private Transform _outputPosition;

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
        Destroy(cube.gameObject);
        Instantiate(_scalableCubePrefab, _outputPosition.position, _outputPosition.rotation);
    }
}
