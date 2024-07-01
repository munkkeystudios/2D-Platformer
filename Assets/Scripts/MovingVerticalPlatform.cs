using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVerticalPlatform : MonoBehaviour
{

    [SerializeField] private Transform point1, point2;
    [SerializeField] private float speed;
    [SerializeField] private bool moveUp;
    


    // Start is called before the first frame update
    void Start()
    {

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
    }

}
