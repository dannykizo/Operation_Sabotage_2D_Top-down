using UnityEngine;
using TopDown.Loading;
using UnityEngine.SceneManagement;

namespace TopDown.UI
{
    public class WinMenu : MonoBehaviour
    {
        [SerializeField] private GameObject winMenu;
        [SerializeField] private GameObject finalLevelWinMenu;

        private void Awake()
        {
            bool finalLevel = SceneManager.GetActiveScene().buildIndex == (int)LoadingManager.Level.Level3;
            winMenu.SetActive(!finalLevel);
            finalLevelWinMenu.SetActive(finalLevel);
        }
    }
}