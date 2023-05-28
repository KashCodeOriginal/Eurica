using Unit.TriggerSystem;
using UnityEngine;

namespace Unit.SceneControllers.Scene4
{
    public class Scene4Ladder : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SoundHelper _scrapeSound;

        // Controlled by PlayerButton.
        public void SetLadderState(bool state)
        {
            _animator.SetBool("Built", state);
        }

        public void PlayScrapeSound()
        {
            _scrapeSound.PlaySound();
        }
    }
}
