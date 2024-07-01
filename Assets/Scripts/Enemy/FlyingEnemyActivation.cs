using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationScript : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void Awake()
    {
        if (enemy == null)
        {
            Debug.LogError("Enemy GameObject is not assigned in " + gameObject.name);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var aiDestinationSetter = enemy.GetComponent<AIDestinationSetter>();
            if (aiDestinationSetter != null)
            {
                aiDestinationSetter.Activate(true);
            }
            else
            {
                Debug.LogError("AIdestinationset component not found on enemy.");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var aiDestinationSetter =enemy.GetComponent<AIDestinationSetter>();
            if (aiDestinationSetter!= null)
            {
                aiDestinationSetter.Activate(false);
            }
            else
            {
                Debug.LogError("AIdestination component not found on enemy.");
            }
        }
    }
}
