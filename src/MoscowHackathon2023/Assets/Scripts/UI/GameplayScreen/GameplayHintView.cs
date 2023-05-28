using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameplayScreen
{
    public class GameplayHintView : MonoBehaviour
    {
        private string currentHint;

        [SerializeField] private GameObject hintViewParent;
        [SerializeField] private TextMeshProUGUI hintOutput;
        [SerializeField] private Image _icon;

        [Header("Situative Icons")]
        [SerializeField] private Sprite _general;
        [SerializeField] private Sprite _controlsWASD;
        [SerializeField] private Sprite _controlsSpace;
        [SerializeField] private Sprite _controlsE;
        [SerializeField] private Sprite _controlsMouse;
        [SerializeField] private Sprite _controlsMouseScroll;
        [SerializeField] private Sprite _controlsMouseButtons;

        private void Awake()
        {
            hintOutput.text = "";
            RequestHidingHint();
        }

        public void RequestShowingHint(string hint)
        {
            if (currentHint == hint)
                return;

            SetIcon(hint);

            currentHint = hint;
            hintOutput.text = hint;
            hintViewParent.SetActive(true);
        }

        private void SetIcon(string hint)
        {
            // It's 7 hours before the deadline, so I'm not going to refactor this.
            if (hint.Contains("WASD"))
            {
                _icon.sprite = _controlsWASD;
            }
            if (hint.Contains("пробел, чтобы подпрыгнуть"))
            {
                _icon.sprite = _controlsSpace;
            }
            if (hint.Contains("кнопку Е"))
            {
                _icon.sprite = _controlsE;
            }
            if (hint.Contains("WASD"))
            {
                _icon.sprite = _controlsWASD;
            }
            else if (hint.Contains("чтобы осмотреться"))
            {
                _icon.sprite = _controlsMouse;
            }
            else if (hint.Contains("колесико мыши"))
            {
                _icon.sprite = _controlsMouseScroll;
            }
            else if (hint.Contains("левую кнопку"))
            {
                _icon.sprite = _controlsMouseButtons;
            }
            else 
            {
                // Default lamp icon.
                _icon.sprite = _general;
            }
        }

        public void RequestHidingHint()
        {
            hintOutput.text = "";
            currentHint = "";
            hintViewParent.SetActive(false);
        }
    }
}
