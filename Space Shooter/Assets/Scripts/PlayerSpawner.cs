using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerSpawner : MonoBehaviour
    {


        // Not needed anymore.



        [SerializeField]
        protected GameObject playerShipObject;

        [SerializeField]
        private float spawnTime = 1;

        [SerializeField]
        private float spawnInvincibilityTime = 3;

        private PlayerSpaceShip playerShip;
        private SpriteRenderer playerShipRenderer;
        private bool playerDestroyed = false;

        private Timer spawnTimer;
        private Timer spawnInvincibilityTimer;
        private Timer invincibilityBlinkTimer;

        private void Awake()
        {
            // Initializes timers
            spawnTimer = new Timer(spawnTime);
            spawnInvincibilityTimer = new Timer(spawnInvincibilityTime);
            invincibilityBlinkTimer = new Timer(0.25f);
        }

        public GameObject SpawnPlayer()
        {
            GameObject spawnedObject = 
                Instantiate(playerShipObject, transform.position, transform.rotation);

            playerShip = spawnedObject.GetComponent<PlayerSpaceShip>();

            if (playerShip == null)
            {
                Debug.LogError("The game object does not have a PlayerSpaceShip component.");
            }
            else
            {
                playerShipRenderer = spawnedObject.GetComponent<SpriteRenderer>();
            }

            return spawnedObject;
        }

        /// <summary>
        /// Respawns the player ship.
        /// </summary>
        private void RespawnPlayer()
        {
            playerShip.transform.position = transform.position;
            playerShip.Health.RestoreMaxHealth();
            playerShip.gameObject.SetActive(true);
            playerDestroyed = false;

            if (spawnInvincibilityTime > 0)
            {
                playerShip.Invulnerable = true;
                spawnInvincibilityTimer.Start();
                invincibilityBlinkTimer.Start();
            }

            // Prints debug info
            Debug.Log("The player ship respawned");
        }

        // Commented out due to changes elsewhere in the code (e.g. lives are moved to GameManager)

        //private void Update()
        //{
        //    // Updates the player ship's invincibility period
        //    if (!playerDestroyed)
        //    {
        //        UpdateInvincibility();
        //    }

        //    // Makes the player ship respawn
        //    // when the spawn time ends
        //    else if (spawnTimer.Check(oneshot: true))
        //    {
        //        RespawnPlayer();
        //    }
        //}

        ///// <summary>
        ///// Updates the player ship's invincibility period.
        ///// </summary>
        //private void UpdateInvincibility()
        //{
        //    // Checks if the player ship was destroyed
        //    if (!playerShip.gameObject.activeSelf)
        //    {
        //        playerDestroyed = true;

        //        // If the player ship has not run out,
        //        // of lives, the spawn timer is started
        //        if (playerShip.Lives > 0)
        //        {
        //            spawnTimer.Start();
        //        }
        //    }
        //    // Updates the player ship's invincibility period
        //    else if (spawnInvincibilityTimer.Active)
        //    {
        //        // Makes the player ship blink
        //        // (turn invisible and visible again repeatedly)
        //        if (invincibilityBlinkTimer.Check(true))
        //        {
        //            playerShipRenderer.enabled = !playerShipRenderer.enabled;
        //            invincibilityBlinkTimer.Start();
        //        }

        //        // Ends the player ship's invincibility
        //        // and blinking when the time ends
        //        if (spawnInvincibilityTimer.Check(true))
        //        {
        //            invincibilityBlinkTimer.Stop();
        //            playerShip.Invulnerable = false;
        //            playerShipRenderer.enabled = true;
        //        }
        //    }
        //}
    }
}
