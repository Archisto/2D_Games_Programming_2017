using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public abstract class SpaceShipBase : MonoBehaviour
    {
        /// <summary>
        /// Backing field for the property speed
        /// </summary>
        [SerializeField]
        private float speed = 2f;

        /// <summary>
        /// Makes the space ship move.
        /// </summary>
        protected abstract void Move();

        public float Speed
        {
            get { return speed; }
            protected set { speed = value; }
        }

        /// <summary>
        /// Updates the space ship.
        /// </summary>
        protected virtual void Update()
        {
            try
            {
                Move();
            }
            catch(System.NotImplementedException e)
            {
                Debug.Log(e.Message);
            }
            catch(System.Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
