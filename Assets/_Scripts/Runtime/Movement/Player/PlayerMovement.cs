using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : Mover
    {
        private void OnMove(InputValue value)
        {
            if (Time.timeScale == 0) return;

            Vector3 initialInput = new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 0);
            currentInput = initialInput.normalized;
        }
    }
}