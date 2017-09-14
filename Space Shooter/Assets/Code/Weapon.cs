using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private float cooldownTime = 0.5f;

        [SerializeField]
        private Projectile projectilePrefab;

        private float timeSinceShot = 0f;
        private bool isInCooldown = false;

        /// <summary>
        /// Makes the weapon fire a projectile.
        /// </summary>
        /// <returns>was the shot successfully fired</returns>
        public bool Shoot()
        {
            // Checks if the weapon is in cooldown phase
            // and if so, returns false for failed shot
            if (isInCooldown)
            {
                return false;
            }

            // Creates a new projectile
            Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

            // Launches the projectile
            projectile.Launch(transform.up);

            // Goes to the cooldown phase
            isInCooldown = true;

            // Resets the time since shot
            timeSinceShot = 0f;

            // Returns true for successful shot
            return true;
        }

        private void Update()
        {
            if (isInCooldown)
            {
                timeSinceShot += Time.deltaTime;
                if (timeSinceShot >= cooldownTime)
                {
                    isInCooldown = false;
                }
            }
        }
    }
}
