﻿using System;
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

        /// <summary>
        /// Is the game object dead
        /// </summary>
        private bool dead = false;

        /// <summary>
        /// Sets the current health value at the start.
        /// </summary>
        private void Start()
        {
            currentHealth = startingHealth;
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
        }

        /// <summary>
        /// Gets whether health has reached the minimum value.
        /// </summary>
        public bool Dead
        {
            get
            {
                return dead;
            }
        }

        /// <summary>
        /// Decreases the current health value.
        /// </summary>
        /// <param name="amount">amount of health lost</param>
        public void DecreaseHealth(int amount)
        {
            if (!dead)
            {
                // Decreases the current health value 
                currentHealth -= amount;

                // Limits the health value to the minimum if it gets too small
                // and sets the game object dead
                if (currentHealth <= minHealth)
                {
                    currentHealth = minHealth;
                    dead = true;
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

                // Sets the game object not dead if it has died
                if (dead)
                {
                    dead = false;
                }
            }
        }
    }
}
