using UnityEngine;
using TopDown.Audio;
using TopDown.Pooling;

namespace TopDown.Explosions
{
    public class ExplosionSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private int maxSize = 10;
        [SerializeField] private AudioClip explosionSound;
        [SerializeField] private float explosionRadius = 0.75f;
        private ObjectPool explosionPool;

        private void Awake()
        {
            explosionPool = ObjectPoolCreator.Instance.CreateObjectPool(gameObject, explosionPrefab, maxSize);
        }
        public void SpawnExplosion()
        {
            //Get explosion object from pool and set its transform
            GameObject explosion = explosionPool.GetObject();

            //Set explosion radius
            if (explosion.TryGetComponent(out Explosion explosionComp))
                explosionComp.SetExplosionRadius(explosionRadius);

            //Play sound
            SoundManager.Instance?.PlayAudio(explosionSound);

            //Activate object and set postion & rotation
            explosion.SetActive(true);
            explosion.transform.position = transform.position;
            explosion.transform.rotation = transform.rotation;
        }

        //Visualize the range of the explosion
        private void OnDrawGizmos()
        {
            //Draw explosion radius
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}