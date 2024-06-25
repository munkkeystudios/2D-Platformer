using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVerticalPlatform : MonoBehaviour
{
    public Transform point1, point2;
    public float speed;
    public bool moveUp;

    private Vector3 previousPosition;
    public Vector3 DeltaMovement { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (moveUp)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }

        if (transform.position.y <= point2.position.y)
        {
            moveUp = true;
        }
        if (transform.position.y >= point1.position.y)
        {
            moveUp = false;
        }

        DeltaMovement = transform.position - previousPosition;
        previousPosition = transform.position;
    }
}

