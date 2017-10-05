using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabToSpawn;

        public GameObject Spawn()
        {
            GameObject spawnedObject = 
                Instantiate(prefabToSpawn, transform.position, transform.rotation);

            return spawnedObject;
        }
    }
}
