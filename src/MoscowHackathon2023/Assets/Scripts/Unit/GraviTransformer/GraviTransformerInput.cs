using Unit.GravityCube;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.GraviTransformer
{
    public class GraviTransformerInput : MonoBehaviour
    {
        public UnityAction<GravityCubeLogic> OnCubeInside;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<GravityCubeLogic>(out var cube))
            {
                cube?.RequestDetach();
                OnCubeInside?.Invoke(cube);
            }
        }
    }
}
