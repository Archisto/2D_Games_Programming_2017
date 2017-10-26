using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Health : MonoBehaviour, IHealth
    {
        /// <summary>
        /// The current health value
        /// </summary>
        private int currentHealth;

        /// <summary>
        /// The health value at the start
        /// </summary>
        [SerializeField]
        private int startingHealth;

        /// <summary>
        /// The maximum health value
        /// </summary>
        [SerializeField]
        private int maxHealth;

        /// <summary>
        /// The minimum health value
        /// </summary>
        [SerializeField]
        private int minHealth;

        private bool isInvincible = false;

        /// <summary>
        /// Sets the current health value at the start.
        /// </summary>
        private void Awake()
        {
            RestoreStartingHealth();
        }

        public void RestoreStartingHealth()
        {
            currentHealth = startingHealth;
        }

        public void RestoreMaxHealth()
        {
            currentHealth = maxHealth;
        }

        /// <summary>
        /// Gets the current health value.
        /// </summary>
        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }

            // set { currentHealth = Mathf.Clamp(value, minHealth, maxHealth); }
        }

        /// <summary>
        /// Gets whether health has reached the minimum value.
        /// </summary>
        public bool IsDead
        {
            get
            {
                return (currentHealth <= minHealth);
            }
        }

        /// <summary>
        /// Decreases the current health value.
        /// </summary>
        /// <param name="amount">amount of health lost</param>
        public void DecreaseHealth(int amount)
        {
            if (!IsDead && !isInvincible)
            {
                // Decreases the current health value 
                currentHealth -= amount;

                // Limits the health value to the minimum if it gets too small
                // and sets the game object dead
                if (currentHealth <= minHealth)
                {
                    currentHealth = minHealth;
                }
            }
        }

        /// <summary>
        /// Increases the current health value.
        /// </summary>
        /// <param name="amount">amount of health gained</param>
        public void IncreaseHealth(int amount)
        {
            if (currentHealth < maxHealth)
            {
                // Increases the current health value
                currentHealth += amount;

                // Limits the health value to the maximum if it gets too large
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }

        public void SetInvincible(bool isInvincible)
        {
            this.isInvincible = isInvincible;
        }
    }
}
