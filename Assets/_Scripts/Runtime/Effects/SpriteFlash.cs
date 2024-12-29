using UnityEngine;
using DG.Tweening;

namespace TopDown.Effects
{
    public class SpriteFlash : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] entitySprites;
        [SerializeField] private float flashDuration = 0.5f;

        public void FlashOnDamage()
        {
            foreach (var sprite in entitySprites)
            {
                sprite.DOColor(Color.red, flashDuration).OnComplete(()=>
                {
                    sprite.DOColor(Color.white, flashDuration);
                });
            }
        }
    }
}