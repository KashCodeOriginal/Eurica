using UnityEngine;

namespace Unit.GraviTransformer
{
    public class GraviTransformerIndicator : MonoBehaviour
    {
        [SerializeField] private bool _isLeftIndicator = true;

        [SerializeField] private Material _glowMat;
        [SerializeField] private Material _offMat;
        [SerializeField] private int _matId;
        private MeshRenderer _mesh;

        public void SetStatus(bool glowLeft, bool glowRight)
        {
            if (!_mesh)
                _mesh = GetComponent<MeshRenderer>();

            if (_isLeftIndicator)
            {
                _mesh.materials[_matId] = glowLeft ? _glowMat : _offMat;
            }
            else
            {
                _mesh.materials[_matId] = glowRight ? _glowMat : _offMat;
            }
        }
    }
}
