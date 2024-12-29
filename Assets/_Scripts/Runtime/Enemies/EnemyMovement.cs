using UnityEngine;

namespace TopDown.Movement
{
    public class EnemyMovement : Mover
    {
        [SerializeField] private EnemyRotation enemyRotation;
        private Vector3 destination;
        private bool hasToMove;
        private float stoppingDistance = 0.1f;

        private void Update()
        {
            if (hasToMove)
            {
                currentInput = (destination - transform.position).normalized;

                if (Vector3.Distance(transform.position, destination) < stoppingDistance)
                {
                    hasToMove = false;
                    currentInput = Vector3.zero;
                }
            }
            else
                currentInput = Vector3.zero;

            enemyRotation.SetMovementTarget(transform.position + currentInput);
        }

        public void MoveToDestination(Vector3 target)
        {
            hasToMove = true;
            destination = target;
        }
        public void CancelMovement()
        {
            hasToMove = false;
            destination = transform.position;
        }
    }
}