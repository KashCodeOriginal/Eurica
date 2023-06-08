using Unit.GraviTransformer;
using Unit.ScaleGun;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.GravityCube
{
    [SelectionBase]
    public class GravityCubeLogic : CubeBase
    {
        public int ColorId { get; private set; }

        [SerializeField] private GravityCubeView _gravityCubeView;

        public void Init(int colorId, Color indicationColor)
        {
            ColorId = colorId;
            _gravityCubeView.SetColor(indicationColor);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if (collision.gameObject.layer == LayerMask.NameToLayer("GravityCubeBlocker"))
            {
                RequestDetach?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GraviTransformerInput input))
            {
                input.SetStatus(true, this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GraviTransformerInput input))
            {
                input.SetStatus(false, this);
            }
        }
    }
}
