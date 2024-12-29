using UnityEngine;

namespace TopDown.Core
{
    public class PauseInput : MonoBehaviour
    {
        //Input methods
        private void OnPause()
        {
            GameManager.Instance.PauseGame();
        }
    }
}