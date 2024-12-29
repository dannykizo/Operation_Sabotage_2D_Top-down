using UnityEngine;
using TopDown.Health;

namespace TopDown.Collectibles
{
    public class ArmorCollectible : CollectibleBase
    {
        [SerializeField] private int armorAmount;

        protected override void PickupCollectible()
        {
            if (collisionObject.TryGetComponent(out HealthComponent health) && health.CanPickupArmor())
            {
                base.PickupCollectible();
                health.AddArmor(armorAmount);
            }
        }
    }
}