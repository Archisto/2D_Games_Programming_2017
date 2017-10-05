using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Health))]
    public abstract class SpaceShipBase : MonoBehaviour, IDamageReceiver
    {
        public enum Type { Player, Enemy }

        /// <summary>
        /// The speed of the space ship.
        /// </summary>
        [SerializeField]
        private float speed = 2f;

        private Weapon[] weapons;

        public Weapon[] Weapons
        {
            get { return weapons; }
        }

        /// <summary>
        /// An autoproperty. Backing fields are automatically protected by the compiler.
        /// </summary>
        public Health Health { get; protected set; }

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
            // Prints debug data
            Debug.Log("SpaceShipBase - Awake");

            weapons = GetComponentsInChildren<Weapon>(includeInactive: true);

            Health = GetComponent<Health>();
        }

        /// <summary>
        /// Gets the type of the unit (either Player or Enemy).
        /// </summary>
        public abstract Type UnitType { get; }

        /// <summary>
        /// Fires a projectile.
        /// </summary>
        protected void Shoot(Type type)
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.Shoot(type);
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
            catch (NotImplementedException e)
            {
                Debug.Log(e.Message);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// Inflicts damage to the space ship.
        /// </summary>
        /// <param name="amount">the amount of damage</param>
        public void TakeDamage(int amount)
        {
            // Decreases the current health
            Health.DecreaseHealth(amount);

            // Prints debug info
            Debug.Log(name + ": " + amount + " damage! HP: "
                + Health.CurrentHealth);

            // Kills the space ship if its HP reaches the minimum value
            if (Health.IsDead)
            {
                Die();
            }
        }

        /// <summary>
        /// Restores health to the space ship.
        /// </summary>
        /// <param name="amount">the amount of health</param>
        public void RestoreHealth(int amount)
        {
            // Decreases the current health
            Health.IncreaseHealth(amount);

            // Prints debug info
            Debug.Log(name + ": +" + amount + " health! HP: "
                + Health.CurrentHealth);
        }

        /// <summary>
        /// Kills the space ship.
        /// </summary>
        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        protected Projectile GetPooledProjectile()
        {
            return LevelController.Current.GetProjectile(UnitType);
        }

        protected bool ReturnPooledProjectile(Projectile projectile)
        {
            return LevelController.Current.ReturnProjectile(UnitType, projectile);
        }

        ///// <summary>
        ///// Checks collisions.
        ///// </summary>
        ///// <param name="other">a collided object's collider</param>
        //protected virtual void OnTriggerEnter2D(Collider2D other)
        //{
        //    // The collided object, maybe a projectile
        //    Projectile projectile = other.gameObject.GetComponent<Projectile>();

        //    // Checks if the collided object is a projectile
        //    if (projectile != null)
        //    {
        //        // Destroys the projectile
        //        Destroy(other.gameObject);

        //        // The space ship takes damage
        //        TakeDamage(projectile);
        //    }
        //    //else
        //    //{
        //    //    // The collided object, maybe an enemy space ship
        //    //    EnemySpaceShip enemy = other.GetComponent<EnemySpaceShip>();
        //    //    Debug.Log(enemy);

        //    //    // Checks if the collided object is an enemy space ship
        //    //    // and this object is not itself an enemy space ship
        //    //    if (enemy != null && GetComponent<EnemySpaceShip>() == null)
        //    //    {
        //    //        // The space ship takes damage
        //    //        TakeDamage(enemy);
        //    //    }
        //    //    // Otherwise no gamage is taken
        //    //    else
        //    //    {
        //    //        Debug.Log("Hit, no damage");
        //    //    }
        //    //}
        //}
    }
}
