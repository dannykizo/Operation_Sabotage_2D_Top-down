using TopDown.Core;
using UnityEngine;

namespace TopDown.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private float zPosition = -10;
        private float displacementReduction = 6;

        private void Update()
        {
            if (Time.timeScale == 0 || GameManager.Instance.GameOver) return;

            //Calculate player and mouse position and change Z position
            Vector3 playerPosition = new Vector3(player.position.x, player.position.y, zPosition);
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, zPosition);

            //Calculate the difference between the mouse position and the player position and divide it(so you can't look too far)
            Vector3 cameraDisplacement = (playerPosition - mousePosition) / displacementReduction;

            //Calculate final camera position and assign it
            Vector3 cameraPosition = playerPosition - cameraDisplacement;
            transform.position = cameraPosition;
        }
    }
}