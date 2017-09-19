using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public abstract class SpaceShipBase : MonoBehaviour
    {
        /// <summary>
        /// The speed of the space ship.
        /// </summary>
        [SerializeField]
        private float speed = 2f;

        //[SerializeField]
        //private Spawner projectileSpawner;

        private Weapon[] weapons;

        public Weapon[] Weapons
        {
            get { return weapons; }
        }

        /// <summary>
        /// Gets or sets the speed of the space ship.
        /// Set is protected.
        /// </summary>
        public float Speed
        {
            get { return speed; }
            protected set { speed = value; }
        }

        protected virtual void Awake()
        {
            weapons = GetComponentsInChildren<Weapon>(includeInactive: true);

            Debug.Log("SpaceShipBase - Awake");
        }

        /// <summary>
        /// Fires a projectile.
        /// </summary>
        protected void Shoot()
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.Shoot();
            }
        }

        /// <summary>
        /// Makes the space ship move.
        /// </summary>
        protected abstract void Move();

        /// <summary>
        /// Updates the space ship.
        /// </summary>
        protected virtual void Update()
        {
            // Attempts to move the space ship
            try
            {
                Move();
            }
            catch (System.NotImplementedException e)
            {
                Debug.Log(e.Message);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// Checks collisions.
        /// </summary>
        /// <param name="other">a collided object's collider</param>
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            // The collided object, maybe a projectile
            Projectile projectile = other.gameObject.GetComponent<Projectile>();

            // Checks if the collided object is a projectile
            if (projectile != null)
            {
                // Destroys the projectile
                Destroy(other.gameObject);

                // The space ship takes damage
                TakeDamage(projectile);
            }
            //else
            //{
            //    // The collided object, maybe an enemy space ship
            //    EnemySpaceShip enemy = other.GetComponent<EnemySpaceShip>();
            //    Debug.Log(enemy);

            //    // Checks if the collided object is an enemy space ship
            //    // and this object is not itself an enemy space ship
            //    if (enemy != null && GetComponent<EnemySpaceShip>() == null)
            //    {
            //        // The space ship takes damage
            //        TakeDamage(enemy);
            //    }
            //    // Otherwise no gamage is taken
            //    else
            //    {
            //        Debug.Log("Hit, no damage");
            //    }
            //}
        }

        /// <summary>
        /// Inflicts damage to the space ship, provided by an object.
        /// </summary>
        /// <param name="damageProvider">the damage provider</param>
        private void TakeDamage(IDamageProvider damageProvider)
        {
            // The space ship's health component
            Health health = GetComponent<Health>();

            // Decreases the current health
            health.DecreaseHealth(damageProvider.GetDamage());

            // If the space ship dies, it is destroyed
            if (health.Dead)
            {
                Destroy(gameObject);
            }

            // Prints debug info
            Debug.Log("Hit! HP: " + health.CurrentHealth);
        }
    }
}
