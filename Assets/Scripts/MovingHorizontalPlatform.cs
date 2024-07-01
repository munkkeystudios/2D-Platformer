using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHorizontalPlatform : MonoBehaviour
{
    [SerializeField] private Transform point1, point2;
    [SerializeField] private float speed;
    [SerializeField] private bool moveRight;

    private Vector3 previousPosition;
    public Vector3 DeltaMovement { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (point1 == null || point2 == null)
        {
            Debug.LogError("One or both of the points are not assigned in " + gameObject.name);
        }
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
        previousPosition = transform.position;
    }
}
