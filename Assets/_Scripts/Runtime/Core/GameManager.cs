using UniRx;
using System;
using UnityEngine;
using TopDown.Audio;
using TopDown.Health;
using TopDown.Loading;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TopDown.Core
{
    public class GameManager : Singleton<GameManager>
    {
        private CompositeDisposable subscriptions = new CompositeDisposable();

        //Events
        public Action OnGameWin { get; set; }
        public Action OnGameLost { get; set; }
        public Action<bool> OnGamePaused { get; set; }
        private bool gamePaused;
        public bool GameOver { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Time.timeScale = 1;
        }

        #region Mission Objectives
        [Header("Level Settings")]
        [SerializeField] private int totalObjectives = 3;
        [SerializeField] private int levelReward = 2500;
        public int LevelReward => levelReward;
        private int completedObjectives;

        [Header("Win/Lose Sounds")]
        [SerializeField] private AudioClip winSound;
        [SerializeField] private AudioClip loseSound;

        public void CompleteMission()
        {
            completedObjectives++;
            if (completedObjectives >= totalObjectives)
            {
                GameOver = true;
                StartCoroutine(GameWin(1));
            }
        }
        private IEnumerator GameWin(float delay)
        {
            yield return new WaitForSeconds(delay);
            SoundManager.Instance?.PlayAudio(winSound);
            yield return new WaitForSeconds(delay);
            OnGameWin?.Invoke();

            //Save progress
            int lastLevel = PlayerPrefs.GetInt("LastLevel", (int)LoadingManager.Level.Level1);
            if (SceneManager.GetActiveScene().buildIndex == lastLevel)
            {
                lastLevel++;
                PlayerPrefs.SetInt("LastLevel", lastLevel);
            }
        }
        private IEnumerator GameLoss(float delay)
        {
            yield return new WaitForSeconds(delay);
            SoundManager.Instance?.PlayAudio(loseSound);
            yield return new WaitForSeconds(delay);
            OnGameLost?.Invoke();
        }
        #endregion

        #region Pausing
        public void PauseGame()
        {
            gamePaused = !gamePaused;
            float timeScaleValue = Convert.ToInt16(!gamePaused);
            Time.timeScale = timeScaleValue;
            completedObjectives = 0;
            OnGamePaused?.Invoke(gamePaused);
        }
        #endregion

        #region Subscribe to player health
        private HealthComponent playerHealth;
        public void SubscribeToPlayer(HealthComponent health)
        {
            //Receive health component and subscribe to it
            playerHealth = health;
            playerHealth.CurrentHealth.ObserveEveryValueChanged(property => property.Value)
                .Subscribe(value =>
                {
                    if (value <= 0)
                    {
                        GameOver = true;
                        StartCoroutine(GameLoss(1));
                    }
                })
                .AddTo(subscriptions);
        }
        private void OnDisable()
        {
            subscriptions.Clear();
        }
        #endregion
    }
}