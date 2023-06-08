using System;
using System.Collections;
using Services.PlaySounds;
using UI.GameplayScreen;
using Unit.TriggerSystem;
using UnityEngine;
using Zenject;

namespace Unit.Cutscene
{
    public class IntroStart : MonoBehaviour
    {
        [SerializeField] private TriggerTaskHelper _triggerTaskHelper;

        private void Start()
        {
            StartCoroutine(IntroTimer());
        }

        private IEnumerator IntroTimer()
        {
            yield return new WaitForSeconds(1f);
            GameplayScreen.Instance?.SetVisibilityOfPlayerUI(false);
            _triggerTaskHelper.StartVoiceMessage("artem_solo_entry");
        }
    }
}
