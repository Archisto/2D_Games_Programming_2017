using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        protected GameObject prefabToSpawn;

        public virtual GameObject Spawn()
        {
            GameObject spawnedObject = 
                Instantiate(prefabToSpawn, transform.position, transform.rotation);

            return spawnedObject;
        }
    }
}
