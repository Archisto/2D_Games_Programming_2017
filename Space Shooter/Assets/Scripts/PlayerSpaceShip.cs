using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerSpaceShip : SpaceShipBase
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
        public const string FIRE_BUTTON_NAME = "Fire1";

        [SerializeField]
        private int lives = 3;

        private AudioSource pickupSound;

        /// <summary>
        /// Gets the type of the unit: Player.
        /// </summary>
        public override Type UnitType
        {
            get
            {
                return Type.Player;
            }
        }

        public int Lives
        {
            get
            {
                return lives;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            // Initializes audio
            pickupSound = GetComponent<AudioSource>();

            if (pickupSound == null)
            {
                Debug.LogError("No AudioSource component found in object PlayerSpaceShip.");
            }
        }

        private Vector3 GetInputVector()
        {
            float horizontalInput = Input.GetAxis(HORIZONTAL_AXIS);
            float verticalInput = Input.GetAxis(VERTICAL_AXIS);

            return new Vector3(horizontalInput, verticalInput);
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

        /// <summary>
        /// Updates the player ship.
        /// </summary>
        protected override void Update()
        {
            base.Update();

            if (Input.GetButton(FIRE_BUTTON_NAME))
            {
                Shoot();
            }
        }

        public void GainLife()
        {
            lives++;
        }

        protected override void Die()
        {
            // Decreases the amount of lives by one
            lives--;

            // The player ship is not deleted but
            // made inactive until it respawns
            gameObject.SetActive(false);

            // Prints debug info
            Debug.Log("The player ship was destroyed. " +
                (lives <= 0 ? "NO LIVES LEFT" : "Lives: " + lives));
        }

        public override void PlaySound(string sound)
        {
            if (sound.Equals("healthItem") && pickupSound != null)
            {
                pickupSound.Play();
            }
            else
            {
                base.PlaySound(sound);
            }
        }
    }
}
