using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationScript : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("Move", true);
            enemy.GetComponent<AIDestinationSetter>().Activate(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.GetComponent<AIDestinationSetter>().Activate(false);
        }
    }
}
