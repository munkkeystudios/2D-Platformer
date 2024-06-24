using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHorizontalPlatform : MonoBehaviour
{
    public Transform point1, point2;
    public float speed;
    public bool moveRight;

    private Vector3 previousPosition;
    public Vector3 DeltaMovement { get; private set; }
    public Vector3 Velocity { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (moveRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (transform.position.x <= point2.position.x)
        {
            moveRight = true;
        }
        if (transform.position.x >= point1.position.x)
        {
            moveRight = false;
        }

        DeltaMovement = transform.position - previousPosition;
        Velocity = DeltaMovement / Time.deltaTime;
        previousPosition = transform.position;
    }
}
