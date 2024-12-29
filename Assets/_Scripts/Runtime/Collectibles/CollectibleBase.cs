using TopDown.Audio;
using UnityEngine;

namespace TopDown.Collectibles
{
    //General logic for collectibles
    //We need a trigger 2D collider and some base logic that detects collisions, checks the layer and disables the pickup
    [RequireComponent(typeof(Collider2D))]
    public class CollectibleBase : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private LayerMask interactionMask;
        protected Collider2D collisionObject;

        [Header("Pickup Sound")]
        [SerializeField] private AudioClip pickupSound;

        private void OnTriggerEnter2D(Collider2D collisionObject)
        {
            if (LayerCheck.IsInLayer(interactionMask, collisionObject))
            {
                this.collisionObject = collisionObject;
                PickupCollectible();
            }
        }

        protected virtual void PickupCollectible()
        {
            gameObject.SetActive(false);
            SoundManager.Instance?.PlayAudio(pickupSound);
        }
    }
}