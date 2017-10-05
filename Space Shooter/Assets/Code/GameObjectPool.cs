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

        protected void Awake()
        {
            pool = new List<GameObject>(poolSize);

            for (int i = 0; i < poolSize; i++)
            {
                AddObject();
            }
        }

        private GameObject AddObject(bool isActive = false)
        {
            GameObject go = Instantiate(objectPrefab);

            if (isActive)
            {
                Activate(go);
            }
            else
            {
                Deactivate(go);
            }

            pool.Add(go);

            return go;
        }

        public bool ReturnObject(GameObject go)
        {
            bool result = false;

            foreach (GameObject pooledObject in pool)
            {
                if (pooledObject == go)
                {
                    Deactivate(go);
                    result = true;

                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Activates the given GameObject.
        /// </summary>
        /// <param name="go">a game object</param>
        protected virtual void Activate(GameObject go)
        {
            go.SetActive(true);
        }

        /// <summary>
        /// Deactivates the given GameObject.
        /// </summary>
        /// <param name="go">a game object</param>
        protected virtual void Deactivate(GameObject go)
        {
            go.SetActive(false);
        }

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

            // If there were no inactive objects in the pool and the
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
