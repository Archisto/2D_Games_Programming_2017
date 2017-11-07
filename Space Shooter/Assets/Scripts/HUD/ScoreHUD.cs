using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    public class ScoreHUD : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI scoreText;

        private int score;

        private void Update()
        {
            UpdateScoreText();
        }

        /// <summary>
        /// Updates the HUD text.
        /// </summary>
        private void UpdateScoreText()
        {
            string text =
                GameManager.Instance.CurrentScore + " pts";

            scoreText.text = text;
        }
    }
}
