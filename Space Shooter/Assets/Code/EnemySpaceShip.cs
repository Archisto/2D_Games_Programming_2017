using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemySpaceShip : SpaceShipBase, IDamageProvider
    {
        [SerializeField]
        private float reachDistance;

        [SerializeField]
        private int damage;

        private GameObject[] movementTargets;
        private int currentMovementTargetIndex = 0;

        public Transform CurrentMovementTarget
        {
            get
            {
                return movementTargets[currentMovementTargetIndex].transform;
            }
        }

        public int GetDamage()
        {
            return damage;
        }

        public void SetMovementTargets(GameObject[] movementTargets)
        {
            this.movementTargets = movementTargets;
            currentMovementTargetIndex = 0;
        }

        protected override void Move()
        {
            if (movementTargets == null || movementTargets.Length == 0)
            {
                Debug.LogError("No movement targets set.");
                return;
            }

            UpdateMovementTarget();

            Vector3 direction = (CurrentMovementTarget.position - transform.position).normalized;
            transform.Translate(Utils.GetMovement(direction, Speed));
        }

        private void UpdateMovementTarget()
        {
            // If the enemy space ship has reached the current movement target,
            // the movement target is updated
            if (Vector3.Distance(transform.position, CurrentMovementTarget.position) < reachDistance)
            {
                currentMovementTargetIndex++;
                currentMovementTargetIndex =
                    Utils.NumberRangeLoopAround(currentMovementTargetIndex, 0, movementTargets.Length - 1);
            }
        }

        protected override void Update()
        {
            base.Update();

            Shoot();
        }
    }
}