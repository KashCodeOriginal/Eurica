using UnityEngine;

namespace Unit.UniversalGun
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class UniversalGunLamp : MonoBehaviour
    {
        private static readonly int EmissiveExposureWeight = Shader.PropertyToID("_EmissiveExposureWeight");

        private SkinnedMeshRenderer _meshRenderer;
        private float _targetValue = 1;
        private float _currentValue = 1;

        private void Awake()
        {
            _meshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        public void SetCurrentState(float value)
        {
            _targetValue = value;
        }

        private void Update()
        {
            _currentValue = Mathf.Lerp(_currentValue, _targetValue, 15f * Time.deltaTime);
            _meshRenderer.material.SetFloat(EmissiveExposureWeight, _currentValue);
        }
    }
}