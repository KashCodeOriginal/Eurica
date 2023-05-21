using System.Collections;
using Unit.TriggerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Unit.Lever
{
    [RequireComponent(typeof(TriggerTaskHelper))]
    public class LeverButtonLogic : MonoBehaviour
    {
        public UnityAction OnPress;
        public UnityAction<bool> OnStateChanged;
        private bool _canPress = true;
        private bool _pressing = false;
        [SerializeField] private float _pressTime = 0.2f;
        [SerializeField] private float _releaseTime = 1f;

        public void TryPressing()
        {
            if (_canPress)
            {
                // Lever acts like a button, press and return to position.
                StartCoroutine(PressAndReturn());
            }
            else
            {
                // Wait till animation ends.
            }
        }

        private IEnumerator PressAndReturn()
        {
            Debug.Log("Pressing lever");

            _canPress = false;

            Press();

            yield return new WaitForSeconds(_pressTime);

            Debug.Log("Releasing lever");

            Release();

            yield return new WaitForSeconds(_releaseTime);

            Debug.Log("Lever can be pressed again");

            _canPress = true;
        }

        public void Press()
        {
            _pressing = true;
            OnPress?.Invoke();
            OnStateChanged?.Invoke(true);
        }

        public void Release()
        {
            _pressing = false;
            OnStateChanged?.Invoke(false);
        }

        public float GetSpeed()
        {
            if (_pressing)
                return 1f / _pressTime;
            else
                return 1f / _releaseTime;
        }
    }
}
