using UnityEngine;
using TopDown.Movement;

namespace TopDown.Animation
{
    [RequireComponent(typeof(Animator))]
    public class LegsAnimation : MonoBehaviour
    {
        [SerializeField] private Mover mover;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            anim.SetBool("moving", mover.CurrentInput != Vector3.zero);
        }
    }
}