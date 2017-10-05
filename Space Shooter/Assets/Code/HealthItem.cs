﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class HealthItem : MonoBehaviour
    {
        /// <summary>
        /// The amount of health given when collected
        /// </summary>
        [SerializeField]
        private int healthBoost;

        /// <summary>
        /// Gets the amount of health given when collected.
        /// </summary>
        /// <returns>the amount of health given when collected</returns>
        public int GetHealthBoost()
        {
            return healthBoost;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            IDamageReceiver healthReceiver = other.GetComponent<IDamageReceiver>();

            if (healthReceiver != null)
            {
                // Restores health to the target
                healthReceiver.RestoreHealth(GetHealthBoost());

                // Destroys the health item
                Destroy(gameObject);
            }
        }
    }
}