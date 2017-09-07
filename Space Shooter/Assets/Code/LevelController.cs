using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private Spawner enemySpawner;

        [SerializeField]
        private GameObject[] enemyMovementTargets;

        /// <summary>
        /// Called first when a Scene is loaded.
        /// </summary>
        protected void Awake()
        {
            if (enemySpawner == null)
            {
                Debug.LogError("No reference to an enemy spawner!");

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

            SpawnEnemyUnit();
        }

        private EnemySpaceShip SpawnEnemyUnit()
        {
            GameObject spawnedEnemyUnit = enemySpawner.Spawn();
            EnemySpaceShip enemyShip = spawnedEnemyUnit.GetComponent<EnemySpaceShip>();
            if (enemyShip != null)
            {
                enemyShip.SetMovementTargets(enemyMovementTargets);
            }

            return enemyShip;
        }
    }
}
