using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class HealthPowerUp : PowerUpBase
    {
        /// <summary>
        /// The amount of health given when collected
        /// </summary>
        [SerializeField]
        private int healthBoost = 5;

        public override PowerUpType Type
        {
            get { return PowerUpType.Health; }
        }

        /// <summary>
        /// Gets the amount of health given when collected.
        /// </summary>
        /// <returns>the amount of health given when collected</returns>
        public int GetHealthBoost()
        {
            return healthBoost;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            PlayerSpaceShip player = other.GetComponent<PlayerSpaceShip>();

            if (player != null)
            {
                player.CollectHealthPowerUp(healthBoost);

                // Destroys the health item
                Destroy(gameObject);
            }
        }
    }
}
