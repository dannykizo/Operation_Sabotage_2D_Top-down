using UnityEngine;

namespace TopDown.Movement
{
    public class EnemyRotation : Rotator
    {
        [Header("Torso")]
        [SerializeField] private Transform torso;

        [Header("Legs")]
        [SerializeField] private Transform legs;

        private Vector3 lookTarget;
        private Vector3 movementTarget;

        private void Update()
        {
            LookAt(transform, lookTarget);
            LookAt(legs, movementTarget);
        }

        public void SetMovementTarget(Vector3 target)
        {
            movementTarget = target;
        }

        public void SetLookTarget(Transform target)
        {
            lookTarget = target.position;
        }
        public void SetLookTarget(Vector3 target)
        {
            lookTarget = target;
        }
    }
}