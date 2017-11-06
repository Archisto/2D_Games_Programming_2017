using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.States;

namespace SpaceShooter
{
    public class GameManager : MonoBehaviour
    {
        #region Statics
        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Instantiate(Resources.Load<GameManager>("GameManager"));
                }

                return instance;
            }
        }
        #endregion Statics

        [SerializeField]
        private int startingLives = 3;

        private int currentLives;

        public int CurrentLives
        {
            get { return currentLives; }
            set
            {
                bool lifeLost = (value < currentLives);

                currentLives = value;

                if (currentLives <= 0)
                {
                    currentLives = 0;
                }

                if (lifeLost && LevelController.Current != null)
                {
                    LevelController.Current.LifeLost(currentLives == 0);
                }
            }
        }

        public int CurrentScore { get; set; }

        public bool GameWon { get; set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Init();
        }

        private void Init()
        {
            DontDestroyOnLoad(gameObject);
            Reset();
            GameWon = false;
        }

        public void Reset()
        {
            currentLives = startingLives;
            CurrentScore = 0;
        }

        public void LevelCompleted()
        {
            GameWon = GameStateController.CurrentState.IsLastLevel;
        }
    }
}