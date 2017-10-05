using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Current
        {
            get; private set;
        }

        [SerializeField]
        private GameObjectPool playerProjectilePool;

        [SerializeField]
        private GameObjectPool enemyProjectilePool;

        [SerializeField]
        private Spawner enemySpawner;

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

        /// <summary>
        /// The current enemy count
        /// </summary>
        private int enemyCount;

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

            if (enemySpawner == null)
            {
                Debug.LogError("No reference to an enemy spawner.");

                enemySpawner = GetComponentInChildren<Spawner>();

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

        public void EnemyDestroyed()
        {
            enemyCount--;
        }

        /// <summary>
        /// Spawns a new enemy with a key press.
        /// </summary>
        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                SpawnEnemyUnit();
            }
        }

        public Projectile GetProjectile(SpaceShipBase.Type type)
        {
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

            // If the result is not null, the projectile it contains is returned
            if (result != null)
            {
                return result.GetComponent<Projectile>();
            }

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
    }
}
