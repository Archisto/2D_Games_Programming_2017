using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color[] availableColors;

    private int currentIndex = 0;

    /// <summary>
    /// Called first when a Scene is loaded.
    /// </summary>
    private void Awake()
    {
        Debug.Log("Awake");

        // Sets the sprite if it's null
        if (sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        // Prints an error message if no colors are available
        if (availableColors.Length == 0)
        {
            Debug.LogError("No colors available!");
        }
    }

    /// <summary>
    /// Called every time the game object is enabled.
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    /// <summary>
    /// Called every time the game object is disabled.
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    /// <summary>
    /// Called just before the game object is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }

    /// <summary>
    /// Initializes the game object.
    /// </summary>
    private void Start()
    {
        Debug.Log("Start");
    }

    /// <summary>
    /// Updates the game object once per frame.
    /// </summary>
	private void Update()
    {
        Debug.Log("Update");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sprite.color = availableColors[currentIndex];
            currentIndex++;
            currentIndex = 
                numberRangeLoopAround(currentIndex, 0, availableColors.Length - 1);
        }
    }

    /// <summary>
    /// Called every physics frame (default: 50 times/second).
    /// </summary>
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }





    private int numberWithinLimits(int number, int min, int max)
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

    private int numberRangeLoopAround(int number, int min, int max)
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
