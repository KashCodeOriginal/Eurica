using UnityEngine;

namespace Unit.GravityCube
{
    [SelectionBase]
    public class GravityCubeLogic : MonoBehaviour
    {
        public int _colorId { get; private set; }
        [SerializeField] private GravityCubeView _gravityCubeView;

        public void Init(int colorId, Color indicationColor)
        {
            _colorId = colorId;
            _gravityCubeView.SetColor(indicationColor);
        }
    }
}
