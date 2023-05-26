using UnityEngine;

namespace Unit.GraviTransformer
{
    public class GraviTransformerIndicator : MonoBehaviour
    {
        [SerializeField] private bool _isLeftIndicator = true;

        [SerializeField] private GameObject _lampGlow;
        [SerializeField] private GameObject _lampOff;

        public void SetStatus(bool glowLeft, bool glowRight)
        {
            if (_isLeftIndicator)
            {
                _lampGlow.SetActive(glowLeft);
                _lampOff.SetActive(!glowLeft);
            }
            else
            {
                _lampGlow.SetActive(glowRight);
                _lampOff.SetActive(!glowRight);
            }
        }
    }
}
