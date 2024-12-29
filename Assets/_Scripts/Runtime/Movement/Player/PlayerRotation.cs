using UnityEngine;

namespace TopDown.Movement
{
    public class PlayerRotation : Rotator
    {
        [Header("Torso")]
        [SerializeField] private Transform torso;
        private Vector3 torsoLookPoint;

        [Header("Legs")]
        [SerializeField] private Transform legs;
        private Vector3 legsLookPoint;

        private Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (Time.timeScale == 0) return;

            // Convert mouse position into world coordinates
            LookAt(torso, MousePosition.GetMousePosition());

            //Make the legs move towards input
            legsLookPoint = transform.position + new Vector3(mover.CurrentInput.x, mover.CurrentInput.y, mover.CurrentInput.y);
            LookAt(legs, legsLookPoint);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(torsoLookPoint, 0.1f);
        }
    }
}