using Unit.GraviTransformer;
using UnityEngine;

namespace Unit.ScaleGun
{
    public class ScaleCubeMK2 : CubeBase
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GraviTransformerInput input))
            {
                input.SetStatus(true, null);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GraviTransformerInput input))
            {
                input.SetStatus(false, null);
            }
        }
    }
}
