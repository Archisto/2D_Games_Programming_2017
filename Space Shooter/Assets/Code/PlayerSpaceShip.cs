using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceShip : MonoBehaviour
{
    public float speed = 1.5f;

    /// <summary>
    /// Updates the game object once per frame.
    /// </summary>
    private void Update()
    {
        // Gets player input and turns it into
        // a movement vector for the game object
        Vector3 movementVector = GetMovementVector();

        // Moves the game object
        transform.Translate(speed * movementVector * Time.deltaTime);
    }

    private Vector3 GetMovementVector()
    {
        // The movement vector that will be returned
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementVector += Vector3.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementVector += Vector3.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementVector += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementVector += Vector3.right;
        }

        return movementVector;
    }
}
