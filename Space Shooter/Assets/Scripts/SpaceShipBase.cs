using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Health))]
    public abstract class SpaceShipBase : MonoBehaviour, IDamageReceiver, ISoundPlayer
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
        /// An auto-implemented property. Backing fields are automatically protected by the compiler.
        /// </summary>
        public Health Health { get; protected set; }

        public bool Invulnerable { get; set; }

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

            // Gets all weapons attached to the space ship
            weapons = GetComponentsInChildren<Weapon>(includeInactive: true);

            // Initializes each weapon
            foreach (Weapon weapon in weapons)
            {
                weapon.Init(this);
            }

            // Initializes health
            Health = GetComponent<Health>();

            // Initializes invincibility
            Invulnerable = false;
        }

        /// <summary>
        /// Gets the type of the unit (either Player or Enemy).
        /// </summary>
        public abstract Type UnitType { get; }

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
        public virtual void TakeDamage(int amount)
        {
            // If the space ship isn't invulnerable, it takes damage
            if (!Invulnerable)
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

        // not protected anymore!
        public Projectile GetPooledProjectile()
        {
            return LevelController.Current.GetProjectile(UnitType);
        }

        // not protected anymore!
        public bool ReturnPooledProjectile(Projectile projectile)
        {
            return LevelController.Current.ReturnProjectile(UnitType, projectile);
        }

        public virtual void PlaySound(string sound)
        {
            
        }
    }
}
