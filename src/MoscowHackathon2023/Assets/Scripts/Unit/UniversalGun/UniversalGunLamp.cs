using UnityEngine;

namespace Unit.UniversalGun
{
    [RequireComponent(typeof(MeshRenderer))]
    public class UniversalGunLamp : MonoBehaviour
    {
        private static readonly int EmissiveExposureWeight = Shader.PropertyToID("_EmissiveExposureWeight");

        private MeshRenderer _meshRenderer;
        private float _targetValue = 1;
        private float _currentValue = 1;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetCurrentState(float value)
        {
            _targetValue = value;
        }

        private void Update()
        {
            _currentValue = Mathf.Lerp(_currentValue, _targetValue, 5f * Time.deltaTime);
            _meshRenderer.material.SetFloat(EmissiveExposureWeight, _currentValue);
        }
    }
}