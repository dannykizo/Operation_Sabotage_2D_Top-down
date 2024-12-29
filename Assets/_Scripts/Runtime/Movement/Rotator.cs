using UnityEngine;

namespace TopDown.Movement
{
    public class Rotator : MonoBehaviour
    {
        protected void LookAt(Transform targetTransform, Vector3 target)
        {
            if (!targetTransform) return;

            float targetAngle = AngleBetweenTwoPoints(targetTransform.position, target) - 90;
            targetTransform.eulerAngles = new Vector3(0, 0, targetAngle);
        }
        private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }
}