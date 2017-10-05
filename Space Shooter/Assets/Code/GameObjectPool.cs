using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField]
        private int poolSize;

        [SerializeField]
        private GameObject objectPrefab;

        [SerializeField]
        private bool shouldGrow;

        private List<GameObject> pool;

        /// <summary>
        /// Initializes the game object pool.
        /// </summary>
        protected void Awake()
        {
            // Initializes the game object pool
            pool = new List<GameObject>(poolSize);

            // Fills the pool with objects
            for (int i = 0; i < poolSize; i++)
            {
                AddObject();
            }
        }

        /// <summary>
        /// Adds a new object to the pool.
        /// </summary>
        /// <param name="isActive">is the object immediately active</param>
        /// <returns>the created game object</returns>
        private GameObject AddObject(bool isActive = false)
        {
            // Creates a new instance of the object
            GameObject go = Instantiate(objectPrefab);

            // Changes the object's activity status
            if (isActive)
            {
                Activate(go);
            }
            else
            {
                Deactivate(go);
            }

            // Adds the created object to the pool 
            pool.Add(go);

            // The pool size is increased if a new
            // object is added after the initial amount
            if (shouldGrow && pool.Count > poolSize)
            {
                poolSize++;
            }

            // Returns the created object
            return go;
        }

        /// <summary>
        /// Returns an object back to the pool.
        /// </summary>
        /// <param name="go">the game object</param>
        /// <returns>could the object be returned back to the pool</returns>
        public bool ReturnObject(GameObject go)
        {
            foreach (GameObject pooledObject in pool)
            {
                if (pooledObject == go)
                {
                    Deactivate(go);

                    return true;
                }
            }

            Debug.LogError("The game object could not be returned to the pool.");
            return false;
        }

        /// <summary>
        /// Activates the given object.
        /// </summary>
        /// <param name="go">a game object</param>
        protected virtual void Activate(GameObject go)
        {
            go.SetActive(true);
        }

        /// <summary>
        /// Deactivates the given object.
        /// </summary>
        /// <param name="go">a game object</param>
        protected virtual void Deactivate(GameObject go)
        {
            go.SetActive(false);
        }

        /// <summary>
        /// Gets a currently unused object from the pool.
        /// </summary>
        /// <returns>a currently unused game object</returns>
        public GameObject GetPoolObject()
        {
            // The result object
            GameObject result = null;

            // Searches for an inactive object in the pool
            // and when one is found, it becomes the result
            for (int i = 0; i < poolSize; i++)
            {
                if (!pool[i].activeSelf)
                {
                    result = pool[i];
                    break;
                }
            }

            // If there are no inactive objects in the pool and the
            // pool should grow, a new object is added to the pool 
            if (result == null && shouldGrow)
            {
                result = AddObject();
            }

            // If an inactive object was found, it is activated
            if (result != null)
            {
                Activate(result);
            }

            // Returns the active result object in the pool
            return result;
        }
    }
}
