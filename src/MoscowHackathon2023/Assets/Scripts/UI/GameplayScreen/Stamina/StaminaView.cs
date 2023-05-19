using UnityEngine;
using UnityEngine.UI;

namespace UI.GameplayScreen.Stamina
{
    public class StaminaView : MonoBehaviour
    {
        [SerializeField] private GameObject _staminaBarParent;
        [SerializeField] private Image _staminaBarIndicator;
        [SerializeField] private GameObject _runningIcon;

        private void Start()
        {
            SetStamina(1f);
            SetRunningIcon(false);
        }

        public void SetStamina(float staminaPercentage)
        {
            if (staminaPercentage > 1f)
            {
                Debug.LogError("Stamina percentage cannot be greater than 1");
                staminaPercentage = 1f;
            }

            if (staminaPercentage == 1f)
            {
                _staminaBarParent.SetActive(false);
            }
            else
            {
                _staminaBarParent.SetActive(true);
                _staminaBarIndicator.fillAmount = staminaPercentage;
            }
        }
        
        public void SetRunningIcon(bool isRunning)
        {
            _runningIcon.SetActive(isRunning);
        }
    }
}
