using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float chaseSpeed;

    private float distance;

    // Update is called once per frame
    void Update()
    {
        distance = (transform.position - player.transform.position).sqrMagnitude;

        if (distance < 4 * 4)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
        }
    }
}
