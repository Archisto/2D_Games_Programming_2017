using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    public class ExtraWeaponHUD : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI extraWeaponText;

        [SerializeField]
        private TextMeshProUGUI extraWeaponTimeText;

        private PlayerSpaceShip player;
        private bool displayText;
        private float extraWeaponTime;

        private void Awake()
        {
            // Hides the extra weapon power-up text by default
            DisplayExtraWeaponText(false, false);
        }

        private void Update()
        {
            // In the case of just starting the game or the
            // player ship dying, the player ship is retrieved
            if (player == null)
            {
                player = LevelController.Current.Player;
            }
            else
            {
                UpdateExtraWeaponTime();
                UpdateExtraWeaponTimeText();
            }
        }

        /// <summary>
        /// Updates the extra weapon power-up time.
        /// </summary>
        private void UpdateExtraWeaponTime()
        {
            // Gets the power-up's remaining time
            extraWeaponTime = player.ExtraWeaponDuration;

            // Is the time 0
            bool outOfTime = (extraWeaponTime == 0);

            // Changes the power-up text's visibility
            // (hidden if the time is 0, otherwise shown)
            DisplayExtraWeaponText(!outOfTime, true);
        }

        /// <summary>
        /// Shows or hides the extra weapon power-up text.
        /// </summary>
        /// <param name="displayed">should the text be displayed</param>
        /// <param name="redundancyCheck">is it checked if the text is
        /// already in the target state</param>
        private void DisplayExtraWeaponText(bool displayed, bool redundancyCheck)
        {
            // Redundancy check
            if (displayText == displayed && redundancyCheck)
            {
                return;
            }

            displayText = displayed;

            // Changes the power-up text's visibility
            extraWeaponText.enabled = displayed;
            extraWeaponTimeText.enabled = displayed;
        }

        /// <summary>
        /// Updates the HUD text.
        /// </summary>
        private void UpdateExtraWeaponTimeText()
        {
            // Sets the text
            // (adds 1 to the time for aesthetic purposes)
            string text =
                (int) (extraWeaponTime + 1) + " s";

            extraWeaponTimeText.text = text;
        }
    }
}
