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
        private float invincibleTime = 1;

        [SerializeField]
        private float blinkInterval = 0.3f;

        private bool extraWeaponPowerUp = false;
        private float extraWeaponDuration;

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

        public float ExtraWeaponDuration
        {
            get
            {
                return extraWeaponDuration;
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

        public void BecomeInvincible()
        {
            var coroutine = StartCoroutine(InvincibleRoutine());
        }

        private IEnumerator InvincibleRoutine()
        {
            Health.SetInvincible(true);

            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            if (sr == null)
            {
                throw new Exception("No renderer found in PlayerSpaceShip object.");
            }

            Color color = sr.color;

            float timer = 0f;
            while (timer < invincibleTime)
            {
                timer += blinkInterval;

                color.a = (color.a == 1 ? 0.3f : 1);
                sr.color = color;

                yield return new WaitForSeconds(blinkInterval);
            }

            color.a = 1;
            sr.color = color;

            Health.SetInvincible(false);
        }

        /// <summary>
        /// Updates the player ship.
        /// </summary>
        protected override void Update()
        {
            base.Update();

            // Shooting
            if (Input.GetButton(FIRE_BUTTON_NAME))
            {
                Shoot();
            }

            // Extra weapon power-up's time
            if (extraWeaponPowerUp)
            {
                extraWeaponDuration -= Time.deltaTime;

                if (extraWeaponDuration <= 0)
                {
                    Debug.Log("Extra weapon power-up lost");

                    ActivateExtraWeaponPowerUp(false);
                    extraWeaponDuration = 0;
                }
            }
        }

        public void GainLife()
        {
            GameManager.Instance.CurrentLives++;
        }

        protected override void Die()
        {
            base.Die();
            GameManager.Instance.CurrentLives--;
        }

        public void CollectHealthPowerUp(int healthBoost)
        {
            Debug.Log("Health power-up collected");

            RestoreHealth(healthBoost);

            // Plays a sound
            pickupSound.Play();
        }

        public void CollectExtraWeaponPowerUp(float duration)
        {
            Debug.Log("Extra weapon power-up collected");

            ActivateExtraWeaponPowerUp(true);
            extraWeaponDuration += duration;

            // Plays a sound
            pickupSound.Play();
        }

        public void ActivateExtraWeaponPowerUp(bool activate)
        {
            // If the power-up's state does not change, nothing happens
            if (extraWeaponPowerUp == activate)
            {
                return;
            }

            // Changes the power-up's state
            extraWeaponPowerUp = activate;

            // All of the player ship's weapons,
            // both active and inactive
            Weapon[] weapons = GetComponentsInChildren<Weapon>(true);

            // The primary weapon's tag
            string primaryWeapon = "PrimaryWeapon";

            foreach (Weapon weapon in weapons)
            {
                // The power-up is activated
                if (activate)
                {
                    // The primary weapon is deactivated
                    // and the extra weapons are activated
                    if (weapon.gameObject.CompareTag(primaryWeapon))
                    {
                        weapon.gameObject.SetActive(false);
                    }
                    else
                    {
                        weapon.gameObject.SetActive(true);
                    }
                }
                // The power-up is deactivated
                else
                {
                    // The primary weapon is activated
                    // and the extra weapons are deactivated
                    if (weapon.gameObject.CompareTag(primaryWeapon))
                    {
                        weapon.gameObject.SetActive(true);
                    }
                    else
                    {
                        weapon.gameObject.SetActive(false);
                    }
                }
            }
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
