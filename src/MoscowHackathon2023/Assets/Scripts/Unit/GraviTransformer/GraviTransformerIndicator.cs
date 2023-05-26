using UnityEngine;

namespace Unit.GraviTransformer
{
    public class GraviTransformerIndicator : MonoBehaviour
    {
        [SerializeField] private bool _isLeftIndicator = true;

        [SerializeField] private Material _glow;
        [SerializeField] private Material _off;
        [SerializeField] private int _matId;
        private MeshRenderer _mesh;

        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
        }

        public void SetStatus(bool glowLeft, bool glowRight)
        {
            if (_isLeftIndicator)
            {
                _mesh.materials[_matId] = glowLeft ? _glow : _off;
            }
            else
            {
                _mesh.materials[_matId] = glowRight ? _glow : _off;
            }
        }
    }
}
