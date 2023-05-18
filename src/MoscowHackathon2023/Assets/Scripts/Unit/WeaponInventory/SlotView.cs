using UnityEngine;
using UnityEngine.UI;

namespace Unit.WeaponInventory
{
    public class SlotView : MonoBehaviour 
    {
        [SerializeField] private Image _weaponImage;
        [SerializeField] private GameObject _higlight;
        
        public void SetIcon(Sprite weaponIcon) 
        {
            _weaponImage.sprite = weaponIcon;            
        }

        public void Highlight() 
        {
            _higlight.SetActive(true);
        }

        public void StopHighlighting() 
        {
            _higlight.SetActive(false);
        }
    }
}
