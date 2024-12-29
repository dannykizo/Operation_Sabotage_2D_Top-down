using UnityEngine;
using TopDown.Health;

namespace TopDown.Explosions
{
    public class Explosion : MonoBehaviour
    {
        private float radius;
        private int explosionDamage = 1000;

        public void SetExplosionRadius(float radius)
        {
            this.radius = radius;
        }

        private void Explode()
        {
            //Harm all objects in explosion range
            Collider2D[] entitiesInRange = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var entity in entitiesInRange)
            {
                if (entity.TryGetComponent(out HealthComponent health))
                    health.ChangeHealth(-explosionDamage, health.transform.position);
            }
        }
    }
}