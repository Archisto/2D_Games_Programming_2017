using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public abstract class SpaceShipBase : MonoBehaviour
    {
        /// <summary>
        /// The speed of the space ship.
        /// </summary>
        [SerializeField]
        private float speed = 2f;

        //[SerializeField]
        //private Spawner projectileSpawner;

        private Weapon[] weapons;

        public Weapon[] Weapons
        {
            get { return weapons; }
        }

        /// <summary>
        /// Gets or sets the speed of the space ship.
        /// Set is protected.
        /// </summary>
        public float Speed
        {
            get { return speed; }
            protected set { speed = value; }
        }

        protected virtual void Awake()
        {
            weapons = GetComponentsInChildren<Weapon>(includeInactive: true);

            Debug.Log("SpaceShipBase - Awake");
        }

        protected void Shoot()
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.Shoot();
            }
        }

        /// <summary>
        /// Makes the space ship move.
        /// </summary>
        protected abstract void Move();

        /// <summary>
        /// Fires a projectile.
        /// </summary>
        //protected abstract void Shoot();

        //protected Projectile FireProjectile()
        //{
        //    GameObject firedProjectile = projectileSpawner.Spawn();
        //    Projectile projectile = firedProjectile.GetComponent<Projectile>();

        //    return projectile;
        //}

        /// <summary>
        /// Updates the space ship.
        /// </summary>
        protected virtual void Update()
        {
            // Attempts to move the space ship
            try
            {
                Move();
            }
            catch (System.NotImplementedException e)
            {
                Debug.Log(e.Message);
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }

            // Attempts to fire a projectile
            //try
            //{
            //    Shoot();
            //}
            //catch (System.Exception e)
            //{
            //    Debug.LogException(e);
            //}
        }
    }
}
