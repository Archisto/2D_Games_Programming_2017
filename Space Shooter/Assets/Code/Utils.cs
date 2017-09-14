using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public static class Utils
    {
        public static Vector3 GetMovement(Vector3 direction, float speed)
        {
            return (direction * speed * Time.deltaTime);
        }

        public static int NumberWithinLimits(int number, int min, int max)
        {
            if (min > max)
            {
                int temp = min;
                min = max;
                max = temp;
            }

            if (number < min)
            {
                return min;
            }
            else if (number > max)
            {
                return max;
            }
            else
            {
                return number;
            }
        }

        public static int NumberRangeLoopAround(int number, int min, int max)
        {
            if (min > max)
            {
                int temp = min;
                min = max;
                max = temp;
            }

            if (number < min)
            {
                return max;
            }
            else if (number > max)
            {
                return min;
            }
            else
            {
                return number;
            }
        }
    }
}
