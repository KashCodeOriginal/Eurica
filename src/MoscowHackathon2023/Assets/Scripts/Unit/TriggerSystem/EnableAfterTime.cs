using UnityEngine;

namespace Unit.TriggerSystem
{
    public class EnableAfterTime : MonoBehaviour
    {
        [SerializeField] private GameObject _enabledAfter;

        public void EnableAfter(float timeout)
        {
            Invoke(nameof(EnableObject), timeout);
        }

        private void EnableObject()
        {
            _enabledAfter.SetActive(true);
        }
    }
}