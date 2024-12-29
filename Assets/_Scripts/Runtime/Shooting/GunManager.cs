using UniRx;
using UnityEngine;
using TopDown.Core;
using TopDown.Audio;
using TopDown.Health;
using TopDown.Pooling;
using TopDown.EnemyAI;

namespace TopDown.Shooting
{
    public class GunManager : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private AudioClip gunSound;
        [SerializeField] private AudioClip emptyClipSound;
        [SerializeField] private AudioClip reloadSound;

        [Header("Cooldown")]
        [SerializeField] private float cooldown = 0.25f;
        private float cooldownTimer;

        [Header("References")]
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Animator muzzleFlash;

        [Header("Ammo")]
        [SerializeField] private int initialAmmo;
        [SerializeField] private int clipSize;

        //Reactive in variables for subscribers
        [HideInInspector] public IntReactiveProperty TotalAmmo = new IntReactiveProperty(0);
        [HideInInspector] public IntReactiveProperty CurrentAmmoInClip = new IntReactiveProperty(0);

        [Header("Pooling")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int maxSize = 100;
        private ObjectPool bulletPool;

        [Header("Noise Range")]
        [SerializeField] private float noiseRange = 2;
        [SerializeField] private LayerMask enemyMask;

        private HealthComponent targetHealth;

        private void Awake()
        {
            //Set total ammo and ammo in clip
            TotalAmmo.Value = initialAmmo;

            if(initialAmmo < clipSize)
                CurrentAmmoInClip.Value = initialAmmo;
            else
                CurrentAmmoInClip.Value = clipSize;

            //Create bullet pool
            bulletPool = ObjectPoolCreator.Instance.CreateObjectPool(gameObject, bulletPrefab, maxSize);
        }
        private void Update()
        {
            cooldownTimer += Time.deltaTime;

            if (targetHealth != null)
            {
                if (CurrentAmmoInClip.Value <= 0) Reload();
                if (targetHealth.CurrentHealth.Value > 0 && CurrentAmmoInClip.Value > 0) Shoot();
            }
        }

        public void SetTarget(HealthComponent target)
        {
            targetHealth = target;
        }
        private void Shoot()
        {
            if (Time.timeScale == 0) return;
            if (GameManager.Instance.GameOver) return;
            if (cooldownTimer < cooldown) return;
            if (CurrentAmmoInClip.Value <= 0)
            {
                SoundManager.Instance?.PlayAudio(emptyClipSound);
                return;
            }

            cooldownTimer = 0;
            CurrentAmmoInClip.Value--;

            //Get bullet from pool
            GameObject bullet = bulletPool.GetObject();
            bullet.GetComponent<Projectile>().ShootBullet(shootPoint);

            //Effects
            SoundManager.Instance?.PlayAudio(gunSound);
            muzzleFlash.SetTrigger("shoot");

            //Alert all enemies in range
            Collider2D[] enemiesInNoiseRange = Physics2D.OverlapCircleAll(transform.position, noiseRange, enemyMask);
            foreach (var enemy in enemiesInNoiseRange)
            {
                if (enemy.TryGetComponent(out EnemyController enemyController))
                    enemyController.AlertEnemy();
            }
        }

        //Input methods
        private void OnAttack()
        {
            Shoot();
        }
        private void OnReload()
        {
            Reload();
        }

        #region Ammo logic
        public void AddAmmo(int amount)
        {
            TotalAmmo.Value = Mathf.Clamp(TotalAmmo.Value + amount, 0, int.MaxValue);
        }
        public bool CanPickupAmmo()
        {
            return true;
        }
        private void Reload()
        {
            if (Time.timeScale == 0) return;
            if (TotalAmmo.Value <= 0) return;

            SoundManager.Instance?.PlayAudio(reloadSound);

            int ammoToReload;
            if (TotalAmmo.Value >= clipSize)
                ammoToReload = clipSize;
            else
                ammoToReload = TotalAmmo.Value;

            CurrentAmmoInClip.Value = ammoToReload;
            TotalAmmo.Value -= ammoToReload;
        }
        #endregion


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, noiseRange);
        }
    }
}