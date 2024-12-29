using UnityEngine;
using TopDown.Health;

namespace TopDown.Collectibles
{
    public class HealthCollectible : CollectibleBase
    {
        [SerializeField] private int healthAmount;

        protected override void PickupCollectible()
        {
            if (collisionObject.TryGetComponent(out HealthComponent health) && health.CanPickupHealth())
            {
                base.PickupCollectible();
                health.Heal(healthAmount);
            }
        }
    }
}