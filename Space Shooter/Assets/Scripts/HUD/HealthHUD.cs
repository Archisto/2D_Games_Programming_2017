using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    public class HealthHUD : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI healthAmountText;

        private Health playerHealth;

        private void Update()
        {
            // In the case of just starting the game or the
            // player ship dying, the player health is retrieved
            if (playerHealth == null)
            {
                playerHealth = LevelController.Current.PlayerHealth;
            }
            // Updates the HUD text
            else
            {
                UpdateHealthText();
            }
        }

        /// <summary>
        /// Updates the HUD text. Shows both the current health and the maximum health.
        /// </summary>
        private void UpdateHealthText()
        {
            string text =
                playerHealth.CurrentHealth + "/" +
                playerHealth.MaxHealth;

            healthAmountText.text = text;
        }
    }
}
