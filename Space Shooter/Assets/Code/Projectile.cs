using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : MonoBehaviour, IDamageProvider
    {
        /// <summary>
        /// The speed of the projectile
        /// </summary>
        [SerializeField]
        private float speed = 8f;

        /// <summary>
        /// The damage dealt by the projectile
        /// </summary>
        [SerializeField]
        private int damage;

        private Rigidbody2D rigidBody;
        private Vector2 direction;
        private bool isLaunched = false;

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            if (rigidBody == null)
            {
                Debug.LogError("No Rigidbody2D component found from the GameObject.");
            }
        }

        public void Launch(Vector2 direction)
        {
            this.direction = direction;
            isLaunched = true;
        }

        public int GetDamage()
        {
            return damage;
        }

        protected void FixedUpdate()
        {
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
