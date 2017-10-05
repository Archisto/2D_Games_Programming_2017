using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IDamageProvider
    {
        /// <summary>
        /// The damage inflicted by the projectile
        /// </summary>
        [SerializeField]
        private int damage;

        ///// <summary>
        ///// The speed of the projectile
        ///// </summary>
        //[SerializeField]
        //private float speed = 8f;

        private float speed = 1;

        private SpaceShipBase.Type projectileType;
        private Rigidbody2D rigidBody;
        private Vector2 direction;
        private bool isLaunched = false;

        /// <summary>
        /// Gets or sets the projectile's type (either Player or Enemy).
        /// </summary>
        public SpaceShipBase.Type ProjectileType
        {
            get { return projectileType; }
            set { projectileType = value; }
        }

        /// <summary>
        /// Called first when a Scene is loaded.
        /// Initializes the projectile.
        /// </summary>
        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            if (rigidBody == null)
            {
                Debug.LogError("No Rigidbody2D component found from the GameObject.");
            }
        }

        /// <summary>
        /// Gets the amount of damage caused by the projectile.
        /// </summary>
        /// <returns>the amount of damage caused by the projectile</returns>
        public int GetDamage()
        {
            return damage;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            IDamageReceiver damageReceiver = other.GetComponent<IDamageReceiver>();

            if (damageReceiver != null)
            {
                // Inflicts damage to the target
                damageReceiver.TakeDamage(GetDamage());

                // Destroys the projectile
                //Destroy(gameObject);

                // Returns the projectile to the pool
                LevelController.Current.ReturnProjectile(ProjectileType, this);
            }
        }

        public void Launch(Vector2 direction, float speed)
        {
            this.direction = direction;
            this.speed = speed;
            isLaunched = true;
        }

        protected void FixedUpdate()
        {
            // If the projectile has not been
            // launched, nothing happens
            if (!isLaunched)
            {
                return;
            }

            Vector2 velocity = direction * speed;
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 newPosition = currentPosition + velocity * Time.fixedDeltaTime;
            rigidBody.MovePosition(newPosition);
        }
    }
}
