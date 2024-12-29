using TopDown.Core;
using UnityEngine;

namespace TopDown.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] protected float speed;

        protected Rigidbody2D body;
        protected Vector3 currentInput;
        public Vector3 CurrentInput => currentInput;

        protected void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        protected void FixedUpdate()
        {
            if (GameManager.Instance.GameOver)
            {
                body.velocity = Vector2.zero;
                return;
            } 

            body.velocity = currentInput * speed * Time.fixedDeltaTime;
        }
    }
}