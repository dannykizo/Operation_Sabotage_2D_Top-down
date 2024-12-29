using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDown.Loading
{
    public class LoadingManager : Singleton<LoadingManager>
    {
        private int lastLevel;
        public int LastLevel => lastLevel;

        public enum Level
        {
            Init = 0,
            Menu = 1,
            Level1 = 2,
            Level2 = 3,
            Level3 = 4,
        }
        private void Start()
        {
            LoadLevel(Level.Menu);

            //Load last gameplay level or set it to level 1 by default
            lastLevel = PlayerPrefs.GetInt("LastLevel", (int)Level.Level1);
        }

        public void LoadLevel(Level level)
        {
            SceneManager.LoadScene(Convert.ToInt32(level));
        }
        public void LoadNextLevel()
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;

            if (currentLevel <= (int)Level.Level2)
                SceneManager.LoadScene(currentLevel + 1);
            else
                LoadLevel(Level.Menu);
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
