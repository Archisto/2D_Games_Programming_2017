using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class ExtraWeaponPowerUp : PowerUpItem
    {
        /// <summary>
        /// The duration of the power-up until its effect ends
        /// </summary>
        [SerializeField]
        private float powerUpDuration = 5;

        public override PowerUpType Type
        {
            get { return PowerUpType.ExtraWeapon; }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            PlayerSpaceShip player = other.GetComponent<PlayerSpaceShip>();

            if (player != null)
            {
                player.CollectExtraWeaponPowerUp(powerUpDuration);

                // Destroys the power-up item
                Destroy(gameObject);
            }
        }
    }
}
