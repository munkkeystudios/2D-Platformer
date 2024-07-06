using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    public GameObject player { get; set; }
    private Enemy enemy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found");
        }
        enemy = GetComponentInParent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError("enemy not properly set");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            enemy.SetStrikingDistanceBool(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            enemy.SetStrikingDistanceBool(false);
        }
    }
}
