using UnityEngine;
using TopDown.Loading;

namespace TopDown.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Menus")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject levelSelectionMenu;

        private void Awake()
        {
            ActivateMenu(mainMenu);
        }
        private void ActivateMenu(GameObject menu)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            levelSelectionMenu.SetActive(false);

            menu.SetActive(true);
        }

        #region Button methods
        public void OpenMenu()
        {
            ActivateMenu(mainMenu);
        }
        public void OpenLevelSelection()
        {
            ActivateMenu(levelSelectionMenu);
        }
        public void OpenOptions()
        {
            ActivateMenu(optionsMenu);
        }
        public void PlayLevel(int index)
        {
            LoadingManager.Instance.LoadLevel((LoadingManager.Level)index);
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        #endregion
    }
}