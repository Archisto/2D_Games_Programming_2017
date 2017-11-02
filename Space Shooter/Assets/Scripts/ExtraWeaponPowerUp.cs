using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class ExtraWeaponPowerUp : PowerUpBase
    {
        /// <summary>
        /// The duration of the power-up until its effect ends
        /// </summary>
        [SerializeField]
        private float duration = 5;

        public override PowerUpType Type
        {
            get { return PowerUpType.ExtraWeapon; }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            PlayerSpaceShip player = other.GetComponent<PlayerSpaceShip>();

            if (player != null)
            {
                player.CollectExtraWeaponPowerUp(duration);

                // Destroys the power-up item
                Destroy(gameObject);
            }
        }
    }
}
