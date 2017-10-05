using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Destroyer : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Projectile projectile = other.GetComponent<Projectile>();

            if (projectile != null)
            {
                LevelController.Current.ReturnProjectile(projectile.ProjectileType, projectile);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}

