using UnityEngine;
using UnityEngine.Events;

namespace Unit.GravityCube
{
    [SelectionBase]
    public class GravityCubeLogic : MonoBehaviour
    {
        public int ColorId { get; private set; }
        public UnityAction RequestDetach;
        
        [SerializeField] private GravityCubeView _gravityCubeView;

        public void Init(int colorId, Color indicationColor)
        {
            ColorId = colorId;
            _gravityCubeView.SetColor(indicationColor);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("GravityCubeBlocker"))
            {
                RequestDetach?.Invoke();
            }
        }
    }
}
