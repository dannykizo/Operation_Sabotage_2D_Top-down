using UniRx;
using UnityEngine;
using TopDown.Pooling;
using UnityEngine.Events;

namespace TopDown.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int initialHealth = 100;
        private int maxHealth;

        [Header("Armor")]
        [SerializeField] private int initialArmor = 0;
        private int maxArmor = 100;

        [Header("Hurt Behaviour")]
        [SerializeField] private UnityEvent hurtBehaviour;

        [Header("Death Behaviour")]
        [SerializeField] private UnityEvent deathBehaviour;

        //Reactive variables
        public IntReactiveProperty CurrentHealth { get; private set; } = new IntReactiveProperty(100);
        public IntReactiveProperty CurrentArmor { get; private set; } = new IntReactiveProperty(0);

        [Header("Pooling")]
        [SerializeField] private GameObject bloodPrefab;
        [SerializeField] private GameObject bloodPoolPrefab;
        [SerializeField] private bool canBleed = true;
        [SerializeField] private int maxSize = 10;
        private ObjectPool bloodParticlePool;
        private ObjectPool bloodPoolParticlePool;

        private void Awake()
        {
            maxHealth = initialHealth;

            //Set initial health
            SetHealth(initialHealth, initialArmor);

            //Create blood particle pool if can bleed
            if (!canBleed || bloodPrefab == null || bloodPoolPrefab == null) return;
            bloodParticlePool = ObjectPoolCreator.Instance.CreateObjectPool(gameObject, bloodPrefab, maxSize);
            bloodPoolParticlePool = ObjectPoolCreator.Instance.CreateObjectPool(gameObject, bloodPoolPrefab, maxSize);
        }

        #region Health Modification/Hurt/Death
        public void Heal(int change)
        {
            int currentHealth = CurrentHealth.Value;
            int currentArmour = CurrentArmor.Value;
            int finalHealth = Mathf.Clamp(currentHealth + change, 0, initialHealth);
            SetHealth(finalHealth, currentArmour);
        }
        public bool CanPickupHealth()
        {
            return CurrentHealth.Value < maxHealth;
        }
        public void AddArmor(int change)
        {
            int currentHealth = CurrentHealth.Value;
            int currentArmour = CurrentArmor.Value;
            int finalArmor = Mathf.Clamp(currentArmour + change, 0, maxArmor);
            SetHealth(currentHealth, finalArmor);
        }
        public bool CanPickupArmor()
        {
            return CurrentArmor.Value < maxArmor;
        }
        public void ChangeHealth(int change, Vector3 damagePoint)
        {
            int healthDamage = 0;
            int currentHealth = CurrentHealth.Value;
            int currentArmour = CurrentArmor.Value;

            //If entity took damage and didn't die call hurt behaviour
            if (change < 0 && currentHealth > 0)
            {
                hurtBehaviour?.Invoke();

                //If can bleed get blood particle & activate it
                if (canBleed)
                {
                    GameObject bloodParticle = bloodParticlePool.GetObject();
                    bloodParticle.transform.position = damagePoint;
                    bloodParticle.transform.rotation = transform.rotation;
                    bloodParticle.SetActive(true);
                }
            }

            //If final result negative, set armor to 0 and subtract the rest from health
            if (CurrentArmor.Value + change < 0)
            {
                healthDamage = CurrentArmor.Value + change;
                currentArmour = 0;
                currentHealth = CurrentHealth.Value + healthDamage;
            }
            //Subtract damage from armor
            else
                currentArmour += change;

            int finalHealth = Mathf.Clamp(CurrentHealth.Value + healthDamage, 0, maxHealth);
            int finalArmour = Mathf.Clamp(currentArmour, 0, maxArmor);

            SetHealth(finalHealth, finalArmour);

            if (currentHealth <= 0) Die();
        }
        private void SetHealth(int health, int armor)
        {
            CurrentHealth.SetValueAndForceNotify(health);
            CurrentArmor.SetValueAndForceNotify(armor);
        }
        private void Die()
        {
            deathBehaviour?.Invoke();

            //If can bleed get blood pool particle, activate it & set position and rotation
            if (canBleed)
            {
                GameObject bloodParticle = bloodPoolParticlePool.GetObject();
                bloodParticle.SetActive(true);
                bloodParticle.transform.position = transform.position;
                bloodParticle.transform.rotation = transform.rotation;
            }
        }
        #endregion
    }
}