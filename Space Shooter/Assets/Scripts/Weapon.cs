using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Weapon : MonoBehaviour
    {
        /// <summary>
        /// The projectile
        /// </summary>
        [SerializeField]
        private Projectile projectilePrefab;

        /// <summary>
        /// The speed of the projectile
        /// </summary>
        [SerializeField]
        private float projectileSpeed = 8f;

        [SerializeField]
        private float waitBeforeShooting = 0;

        [SerializeField]
        private float cooldownTime = 0.5f;

        private float elapsedWaitTime = 0f;
        private bool initialWaitOver = false;
        private bool isInCooldown = false;

        /// <summary>
        /// The spaceship which owns this weapon
        /// </summary>
        private SpaceShipBase owner;

        public void Init(SpaceShipBase owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Gets the weapon's owner's type (either Player or Enemy).
        /// </summary>
        public SpaceShipBase.Type ProjectileType
        {
            get { return owner.UnitType; }
        }

        /// <summary>
        /// Makes the weapon fire a projectile.
        /// </summary>
        /// <returns>was the shot successfully fired</returns>
        public bool Shoot()
        {
            // Checks if the weapon is in cooldown phase
            // and if so, returns false for canceled shot
            if (isInCooldown || !initialWaitOver)
            {
                return false;
            }

            // Gets an inactive projectile from the projectile pool
            Projectile projectile = owner.GetPooledProjectile();

            // If there is a projectile, it is initialized and launched
            if (projectile != null)
            {
                // Sets the projectile's starting position
                projectile.transform.position = transform.position;

                // Sets the projectile's rotation
                projectile.transform.rotation = transform.rotation;

                // Launches the projectile
                projectile.Launch(this, transform.up, projectileSpeed);

                // Goes to the cooldown phase
                isInCooldown = true;

                // Resets the time since shot
                elapsedWaitTime = 0f;

                // The shot is successful -> returns true
                return true;
            }

            // The shot is unsuccessful -> returns false
            return false;
        }

        public bool DisposeProjectile(Projectile projectile)
        {
            return owner.ReturnPooledProjectile(projectile);
        }

        private void Update()
        {
            if (!initialWaitOver)
            {
                elapsedWaitTime += Time.deltaTime;
                if (elapsedWaitTime >= waitBeforeShooting)
                {
                    initialWaitOver = true;
                }
            }

            if (isInCooldown)
            {
                elapsedWaitTime += Time.deltaTime;
                if (elapsedWaitTime >= cooldownTime)
                {
                    isInCooldown = false;
                }
            }
        }
    }
}
