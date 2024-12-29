using UnityEngine;
using TopDown.UI;

namespace TopDown.Audio
{
    public class HurtFeedback : MonoBehaviour
    {
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private DamageScreenFlash damageFlash;

        public void PlayHurtFeedback()
        {
            damageFlash?.StartFlash();
            SoundManager.Instance?.PlayAudio(hurtSound);
        }
    }
}