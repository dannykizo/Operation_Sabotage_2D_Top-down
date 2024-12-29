using UnityEngine;
using TopDown.Health;
using TopDown.Movement;
using TopDown.Shooting;

namespace TopDown.EnemyAI
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private Transform player;

        //References to components
        private EnemyRotation enemyRotation;
        private EnemyMovement enemyMovement;
        private GunManager enemyGun;
        private bool alerted;

        //Movement positions
        private Vector3 initialPosition;
        private Vector3 lastPlayerPosition;

        //Shot delay
        private float shotDelay = 0.1f;
        private float delayTimer;

        private void Awake()
        {
            //Get all behaviour components
            enemyGun = GetComponent<GunManager>();
            enemyRotation = GetComponent<EnemyRotation>();
            enemyMovement = GetComponent<EnemyMovement>();

            //Set initial position
            initialPosition = transform.position;
        }
        private void Update()
        {
            //Player in sight
            if (Vector3.Distance(transform.position, player.transform.position) <= range)
                AttackState();
            else
            {
                if (alerted)
                    ChaseState();
                else
                    IdleState();
            }
        }

        #region Behaviour states
        private void AttackState()
        {
            //Cancel movement
            enemyMovement.CancelMovement();

            //Alert enemy, rotate towards player and start shooting
            AlertEnemy();

            enemyRotation.SetLookTarget(player);

            delayTimer += Time.deltaTime;
            if (delayTimer > shotDelay)
            {
                if (player.TryGetComponent(out HealthComponent health))
                    enemyGun.SetTarget(health);
                delayTimer = 0;
            }
        }
        private void ChaseState()
        {
            //Clear target
            enemyGun.SetTarget(null);

            //Move & rotate to last player position
            enemyMovement.MoveToDestination(lastPlayerPosition);
            enemyRotation.SetLookTarget(lastPlayerPosition);
        }
        private void IdleState()
        {
            //Clear target
            enemyGun.SetTarget(null);

            //Wait X seconds and move to initial position
            enemyMovement.MoveToDestination(initialPosition);
        }
        #endregion

        //When the enemy gets hurt alert him and set movement to player
        public void AlertEnemy()
        {
            alerted = true;
            SetPlayerPosition();
        }
        private void SetPlayerPosition()
        {
            lastPlayerPosition = player.transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}