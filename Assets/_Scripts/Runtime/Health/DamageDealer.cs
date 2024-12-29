using UnityEngine;

namespace TopDown.Health
{
    public class DamageDealer : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private int damage;
        [SerializeField] private LayerMask damageMask;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.SetActive(false);

            HealthComponent health = null;
            if (LayerCheck.IsInLayer(damageMask, collision) && collision.TryGetComponent(out health))
                health.ChangeHealth(-damage, collision.ClosestPoint(transform.position));
        }
    }
}