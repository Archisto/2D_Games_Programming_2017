using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter.States;

namespace SpaceShooter
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Current
        {
            get; private set;
        }

        [SerializeField]
        private Spawner playerSpawner;

        [SerializeField]
        private Spawner enemySpawner;

        [SerializeField]
        private GameObjectPool playerProjectilePool;

        [SerializeField]
        private GameObjectPool enemyProjectilePool;

        [SerializeField]
        private GameObject[] enemyMovementTargets;

        /// <summary>
        /// The time before the first spawn
        /// </summary>
        [SerializeField, Tooltip("The time before the first spawn.")]
        private float waitBeforeSpawning;

        /// <summary>
        /// How often should an enemy be spawned
        /// (measured in seconds)
        /// </summary>
        [SerializeField, Tooltip("The time between spawning enemies.")]
        private float spawnInterval = 1;

        /// <summary>
        /// How many enemies can simultanenously exist
        /// </summary>
        [SerializeField,
            Tooltip("The maximum amount of simultaneously existing enemies.")]
        private int maxConcurrentEnemyCount = 10;

        [SerializeField]
        private int targetEnemiesKilled = 5;

        [SerializeField, Range(0, 1000)]
        private int scoreFromKill;

        [SerializeField, Range(-1000, 0)]
        private int scorePenaltyFromDeath;

        [SerializeField, Range(0.0f, 1.0f)]
        private float itemDropChance;

        [SerializeField]
        private GameObject healthPowerUp;

        [SerializeField]
        private GameObject extraWeaponPowerUp;

        [SerializeField]
        private GameStateType nextState;

        /// <summary>
        /// The current enemy count
        /// </summary>
        private int enemyCount;

        /// <summary>
        /// The amount of killed enemies
        /// </summary>
        private int killedEnemies;

        /// <summary>
        /// Called first when a Scene is loaded or the object is created.
        /// </summary>
        protected void Awake()
        {
            if (Current == null)
            {
                Current = this;
            }
            else
            {
                Debug.LogError("There are multiple LevelControllers in the scene.");
            }

            if (playerSpawner == null)
            {
                Debug.LogError("No reference to a player spawner.");

                //playerSpawner = GetComponentInChildren<Spawner>();
                //playerSpawner = GetComponentInChildren<PlayerSpawner>();
            }

            if (enemySpawner == null)
            {
                Debug.LogError("No reference to an enemy spawner.");

                //enemySpawner = GetComponentInChildren<Spawner>();

                // Esimerkkejä objektin etsimiselle:

                //enemySpawner = GameObject.FindObjectOfType<Spawner>();

                //Transform childTransform = transform.Find("EnemySpawner");
                //if (childTransform != null)
                //{
                //    enemySpawner = childTransform.gameObject.GetComponent<Spawner>();
                //}

                //enemySpawner = transform.Find("EnemySpawner").gameObject.GetComponent<Spawner>();
            }
        }

        protected void Start()
        {
            SpawnPlayer();
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            // Wait for a while before spawning the first enemy
            yield return new WaitForSeconds(waitBeforeSpawning);

            while (true)
            {
                if (enemyCount < maxConcurrentEnemyCount)
                {
                    // Spawns an enemy
                    EnemySpaceShip enemy = SpawnEnemyUnit();

                    // If an enemy was successfully spawned,
                    // the enemy count is increased
                    if (enemy != null)
                    {
                        enemyCount++;
                    }
                    else
                    {
                        Debug.LogError("Could not spawn an enemy.");
                        yield break;
                    }
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private PlayerSpaceShip SpawnPlayer()
        {
            PlayerSpaceShip playerShip = null;
            GameObject playerObject = playerSpawner.Spawn();

            if (playerObject != null)
            {
                playerShip = playerObject.GetComponent<PlayerSpaceShip>();
            }

            playerShip.BecomeInvincible();

            return playerShip;
        }

        private EnemySpaceShip SpawnEnemyUnit()
        {
            EnemySpaceShip enemyShip = enemySpawner.Spawn().
                GetComponent<EnemySpaceShip>();

            if (enemyShip != null)
            {
                enemyShip.SetMovementTargets(enemyMovementTargets);
            }

            return enemyShip;
        }

        public void EnemyDestroyed(Vector3 enemyPosition)
        {
            // Decreases the amount of enemies in the level
            enemyCount--;

            // Increases the amount of killed enemies
            killedEnemies++;

            // Gives score
            GameManager.Instance.CurrentScore += scoreFromKill;

            // Determines the dropped power-up, if any
            DropPowerUp(enemyPosition);

            // If enough enemies have been killed, the level is completed
            if (killedEnemies >= targetEnemiesKilled)
            {
                GameManager.Instance.LevelCompleted();
                GoToState(nextState);
            }
        }

        private void DropPowerUp(Vector3 position)
        {
            // Only if drop chance is above 0 can a power-up item be dropped
            if (itemDropChance > 0)
            {
                GameObject powerUpItem;

                float random = Random.Range(0f, 1f);

                if (random <= itemDropChance)
                {
                    random = Random.Range(0f, 1f);

                    // Health power-up
                    if (random > 0.5f)
                    {
                        powerUpItem = Instantiate(healthPowerUp);
                    }
                    // Extra weapon power-up
                    else
                    {
                        powerUpItem = Instantiate(extraWeaponPowerUp);
                    }

                    powerUpItem.transform.position = position;
                }
            }
        }

        private void GoToState(GameStateType state)
        {
            Debug.Log("Transitioning to " + state);
            GameStateController.PerformTransition(state);
        }

        public void Update()
        {
            // Spawns a new enemy unit with a key press
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                SpawnEnemyUnit();
            }
        }

        public Projectile GetProjectile(SpaceShipBase.Type type)
        {
            // The result object
            GameObject result = null;

            // Tries to get a projectile from the correct pool
            if (type == SpaceShipBase.Type.Player)
            {
                result = playerProjectilePool.GetPoolObject();
            }
            else
            {
                result = enemyProjectilePool.GetPoolObject();
            }

            // If the result is not null, its Projectile component is returned
            if (result != null)
            {
                Projectile projectile = result.GetComponent<Projectile>();

                // If there's no Projectile component, prints an error message
                if (projectile == null)
                {
                    Debug.LogError("Projectile component could not be found" +
                                   " from the object fetched from the pool");
                }

                return projectile;
            }

            // Returns null if there's no result
            return null;
        }

        public bool ReturnProjectile(SpaceShipBase.Type type, Projectile projectile)
        {
            if (type == SpaceShipBase.Type.Player)
            {
                return playerProjectilePool.ReturnObject(projectile.gameObject);
            }
            else
            {
                return enemyProjectilePool.ReturnObject(projectile.gameObject);
            }
        }

        public void LifeLost(bool outOfLives)
        {
            // If the player runs out of lives,
            // the game over screen is displayed
            if (outOfLives)
            {
                GameManager.Instance.GameWon = false;
                GoToState(GameStateType.GameOver);
            }
            else
            {
                // Lowers score
                GameManager.Instance.CurrentScore += scorePenaltyFromDeath;

                SpawnPlayer();
            }
        }
    }
}