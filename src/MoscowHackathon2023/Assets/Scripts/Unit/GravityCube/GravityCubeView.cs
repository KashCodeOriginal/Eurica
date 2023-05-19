using UnityEngine;

namespace Unit.GravityCube
{
    public class GravityCubeView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshColorIndication;
        [SerializeField] private int meshMatId = 0;

        public void SetColor(Color color)
        {
            meshColorIndication.materials[meshMatId].SetColor("_EmissiveColor", color * 100);
        }
    }
}