using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace TopDown.UI
{
    public class DamageScreenFlash : MonoBehaviour
    {
        [Header("Fade Parameters")]
        [SerializeField] private float finalOpacity = 0.2f;
        [SerializeField] private float flashSpeed = 0.5f;
        [SerializeField] private float delay = 0.5f;

        private Tween damageFlashTween;
        private Image img;

        private void Awake()
        {
            img = GetComponent<Image>();
            img.DOFade(0, 0);
        }

        public void StartFlash()
        {
            //Kill previous tween
            damageFlashTween?.Kill();

            //Reset image transparency
            damageFlashTween = img.DOFade(0, 0).OnComplete(()=>
            {
                //Fade image in to show hurt effect
                img.DOFade(finalOpacity, flashSpeed).OnComplete(() =>
                {
                    //Fade out after a short delay
                    img.DOFade(0, flashSpeed).SetDelay(delay);
                });
            });
        }
    }
}