using UnityEngine;
using TopDown.Core;
using TopDown.Loading;
using UnityEngine.SceneManagement;

namespace TopDown.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Menus")]
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private GameObject gameWinUI;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject optionsUI;

        [Header("Next Mission Button")]
        [SerializeField] private GameObject nextMissionButton;

        private void Awake()
        {
            //Activate game UI by default
            ActivateMenu(gameUI);

            //If last level deactivate next mission button
            if (SceneManager.GetActiveScene().buildIndex == (int)LoadingManager.Level.Level3)
                nextMissionButton.SetActive(false);
        }

        //Subscribe/unsubscribe from player health component
        private void OnEnable()
        {
            GameManager.Instance.OnGameWin += OnGameWon;
            GameManager.Instance.OnGameLost += OnGameOver;
            GameManager.Instance.OnGamePaused += OnGamePaused;
        }
        private void OnDisable()
        {
            GameManager.Instance.OnGameWin -= OnGameWon;
            GameManager.Instance.OnGameLost -= OnGameOver;
            GameManager.Instance.OnGamePaused -= OnGamePaused;
        }

        private void OnGamePaused(bool status)
        {
            if (status) ActivateMenu(pauseUI);
            else ActivateMenu(gameUI);
        }
        private void OnGameOver()
        {
            ActivateMenu(gameOverUI);
        }
        private void OnGameWon()
        {
            ActivateMenu(gameWinUI);
        }

        private void ActivateMenu(GameObject menu)
        {
            gameUI.SetActive(false);
            gameWinUI.SetActive(false);
            gameOverUI.SetActive(false);
            pauseUI.SetActive(false);
            optionsUI.SetActive(false);

            menu.SetActive(true);
        }

        //Public methods
        public void Restart()
        {
            LoadingManager.Instance.RestartLevel();
        }
        public void MainMenu()
        {
            LoadingManager.Instance.LoadLevel(LoadingManager.Level.Menu);
        }
        public void OpenOptions()
        {
            ActivateMenu(optionsUI);
        }
        public void OpenPauseMenu()
        {
            ActivateMenu(pauseUI);
        }
        public void UnpauseGame()
        {
            GameManager.Instance.PauseGame();
        }
        public void NextMission()
        {
            LoadingManager.Instance.LoadNextLevel();
        }
    }
}