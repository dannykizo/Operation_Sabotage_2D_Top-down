using UnityEngine;
using TopDown.Shooting;

namespace TopDown.Collectibles
{
    public class AmmoCollectible : CollectibleBase
    {
        [SerializeField] private int ammoAmount;

        protected override void PickupCollectible()
        {
            if (collisionObject.TryGetComponent(out GunManager gunManager) && gunManager.CanPickupAmmo())
            {
                base.PickupCollectible();
                gunManager.AddAmmo(ammoAmount);
            }
        }
    }
}