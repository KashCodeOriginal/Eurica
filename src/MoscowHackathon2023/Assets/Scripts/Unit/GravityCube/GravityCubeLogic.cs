using UnityEngine;

namespace Unit.GravityCube
{
    [SelectionBase]
    public class GravityCubeLogic : MonoBehaviour
    {
        public int ColorId { get; private set; }
        
        [SerializeField] private GravityCubeView _gravityCubeView;

        public void Init(int colorId, Color indicationColor)
        {
            ColorId = colorId;
            _gravityCubeView.SetColor(indicationColor);
        }
    }
}
