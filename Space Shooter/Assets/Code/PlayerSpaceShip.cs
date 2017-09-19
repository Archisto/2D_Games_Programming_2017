using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerSpaceShip : SpaceShipBase
    {
        //private float horSpeedModifier = 2f;
        //private float vertSpeedModifier = 1f;

        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
        public const string FIRE_BUTTON_NAME = "Fire1";

        private Vector3 GetInputVector()
        {
            // The movement vector that will be returned
            Vector3 movementVector = Vector3.zero;

            float horizontalInput = Input.GetAxis(HORIZONTAL_AXIS);
            float verticalInput = Input.GetAxis(VERTICAL_AXIS);

            return new Vector3(horizontalInput, verticalInput);
    
            //if (Input.GetKey(KeyCode.W))
            //{
            //    movementVector += Vector3.up * vertSpeedModifier;
            //}

            //if (Input.GetKey(KeyCode.S))
            //{
            //    movementVector += Vector3.down * vertSpeedModifier;
            //}

            //if (Input.GetKey(KeyCode.A))
            //{
            //    movementVector += Vector3.left * horSpeedModifier;
            //}

            //if (Input.GetKey(KeyCode.D))
            //{
            //    movementVector += Vector3.right * horSpeedModifier;
            //}

            //return movementVector;
        }

        /// <summary>
        /// Moves the space ship.
        /// </summary>
        protected override void Move()
        {
            // Gets player input and turns it into
            // a movement vector for the game object
            Vector3 inputVector = GetInputVector();

            // Moves the game object
            transform.Translate(Utils.GetMovement(inputVector, Speed));
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetButton(FIRE_BUTTON_NAME))
            {
                Shoot();
            }
        }

        /// <summary>
        /// Checks collisions.
        /// </summary>
        /// <param name="other">a collided object's collider</param>
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            // Checks harmful collisions
            base.OnTriggerEnter2D(other);

            // The space ship's health component
            Health health = GetComponent<Health>();

            // The collided object, maybe a health item
            HealthItem healthItem = other.gameObject.GetComponent<HealthItem>();

            // Checks if the collided object is a health item
            if (healthItem != null)
            {
                // Destroys the health item
                Destroy(other.gameObject);

                // Increases the current health
                health.IncreaseHealth(healthItem.GetHealthBoost());

                // Prints debug info
                Debug.Log("Health boost! HP: " + health.CurrentHealth);
            }
        }
    }
}
