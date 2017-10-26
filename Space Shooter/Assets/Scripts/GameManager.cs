using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                currentLives = value;

                if (currentLives <= 0)
                {
                    currentLives = 0;
                }

                if (LevelController.Current != null)
                {
                    LevelController.Current.LifeLost(currentLives);
                }
            }
        }

        public int CurrentScore { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
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
        }

        public void Reset()
        {
            currentLives = startingLives;
            CurrentScore = 0;
        }
    }
}