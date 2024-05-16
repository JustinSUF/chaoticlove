using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plafrommoveup : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxDistance = 10.0f;

    private Vector3 startPosition;
    private bool movingForward = true;

    void Start()
    {
        // Save the starting position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position of the platform
        float movement = Mathf.PingPong(Time.time * speed, maxDistance);
        Vector3 newPosition;

        if (movingForward)
        {
            newPosition = startPosition + new Vector3(0, movement, 0);

            if (movement >= maxDistance)
            {
                movingForward = false;
            }
        }
        else
        {
            newPosition = startPosition + new Vector3(0, maxDistance - movement, 0);

            if (movement <= 0)
            {
                movingForward = true;
            }
        }

        // Move the platform to the new position
        transform.position = newPosition;
    }
}
