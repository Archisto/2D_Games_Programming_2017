using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public enum PowerUpType
    {
        Health = 0,
        ExtraWeapon = 1
    }

    public abstract class PowerUpBase : MonoBehaviour
    {
        /// <summary>
        /// The time until the power-up item disappears (in seconds)
        /// </summary>
        [SerializeField]
        private float lifetime = 5;

        private float elapsedTime = 0;

        public abstract PowerUpType Type { get; }

        private void Update()
        {
            UpdateLifeTime();
        }

        /// <summary>
        /// Updates the power-up item's lifetime.
        /// When the time is up, the item is destroyed.
        /// </summary>
        private void UpdateLifeTime()
        {
            elapsedTime += Time.deltaTime;

            // If time is up, the power-up item is destroyed
            if (elapsedTime > lifetime)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            PlayerSpaceShip player = other.GetComponent<PlayerSpaceShip>();

            if (player != null)
            {
                // Destroys the power-up item
                Destroy(gameObject);
            }
        }
    }
}
