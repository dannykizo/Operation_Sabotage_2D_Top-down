using UnityEngine;

namespace TopDown.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [Header("Movement Stats")]
        [SerializeField] private float speed;
        [SerializeField] private float lifetime;
        private Rigidbody2D body;
        private float lifeTimer;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        public void ShootBullet(Transform shootPoint)
        {
            lifeTimer = 0;
            body.velocity = Vector2.zero;
            transform.position = shootPoint.position;
            transform.rotation = shootPoint.rotation;

            gameObject.SetActive(true);

            //Add force to make bullet move forward
            body.AddForce(-transform.up * speed, ForceMode2D.Impulse);
        }

        private void Update()
        {
            //Deactivate projectile after X seconds
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= lifetime)
                ResetProjectile();
        }
        private void ResetProjectile()
        {
            gameObject.SetActive(false);
            lifeTimer = 0;
        }
    }
}