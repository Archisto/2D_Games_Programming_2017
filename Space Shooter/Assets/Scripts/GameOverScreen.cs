using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceShooter.States;
using TMPro;

namespace SpaceShooter
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private GameStateType nextState;

        [SerializeField]
        private TextMeshProUGUI resultText;

        [SerializeField]
        private TextMeshProUGUI scoreText;

        private void Awake()
        {
            SetResultText();
            SetScoreText();
        }

        private void SetResultText()
        {
            string text;

            if (GameManager.Instance.GameWon)
            {
                text = "Congratulation!! A winner is you!";
            }
            else
            {
                text = "Game Over!";
            }

            resultText.text = text;
        }

        private void SetScoreText()
        {
            string text = "Score: " + GameManager.Instance.CurrentScore;

            scoreText.text = text;
        }

        public void GoToMainMenu()
        {
            GameStateController.PerformTransition(nextState);
        }
    }
}
