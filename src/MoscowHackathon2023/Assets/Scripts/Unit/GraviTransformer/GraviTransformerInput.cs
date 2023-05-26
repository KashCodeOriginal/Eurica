using Unit.GravityCube;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.GraviTransformer
{
    public class GraviTransformerInput : MonoBehaviour
    {
        public UnityAction<bool, GravityCubeLogic> OnCubeInside;

        public void SetStatus(bool inside, GravityCubeLogic cube)
        {
            OnCubeInside?.Invoke(inside, cube);
        }
    }
}
