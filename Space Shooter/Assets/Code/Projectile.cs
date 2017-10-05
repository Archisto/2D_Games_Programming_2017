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

        private Weapon weapon;

        private float speed = 1;

        //private SpaceShipBase.Type projectileType;
        private Rigidbody2D rigidBody;
        private Vector2 direction;
        private bool isLaunched = false;

        private AudioSource shootSound;

        /// <summary>
        /// Gets the projectile's type (either Player or Enemy).
        /// </summary>
        public SpaceShipBase.Type ProjectileType
        {
            get { return weapon.ProjectileType; }

            //get { return projectileType; }
            //set { projectileType = value; }
        }

        /// <summary>
        /// Called first when a Scene is loaded.
        /// Initializes the projectile.
        /// </summary>
        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            shootSound = GetComponent<AudioSource>();

            if (rigidBody == null)
            {
                Debug.LogError("No Rigidbody2D component found in object Projectile.");
            }

            if (shootSound == null)
            {
                Debug.LogError("No AudioSource component found in object Projectile.");
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
            IDamageReceiver damageReceiver =
                other.GetComponent<IDamageReceiver>();

            if (damageReceiver != null)
            {
                // Inflicts damage to the target
                damageReceiver.TakeDamage(GetDamage());

                // Returns the projectile back to the pool
                //LevelController.Current.ReturnProjectile(ProjectileType, this);

                if ( !weapon.DisposeProjectile(this) )
                {
                    Debug.LogError("Could not return the projectile to the pool.");

                    // Destroys the projectile
                    Destroy(gameObject);
                }
            }
        }

        public void Launch(Weapon weapon, Vector2 direction, float speed)
        {
            this.weapon = weapon;
            this.direction = direction;
            this.speed = speed;
            isLaunched = true;

            // Plays a sound
            if (shootSound != null)
            {
                shootSound.Play();
            }
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
