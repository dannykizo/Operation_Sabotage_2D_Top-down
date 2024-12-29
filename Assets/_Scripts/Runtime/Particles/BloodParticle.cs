using UnityEngine;

namespace TopDown.Particles
{
    public class BloodParticle : MonoBehaviour
    {
        public void DeactivateBlood()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}