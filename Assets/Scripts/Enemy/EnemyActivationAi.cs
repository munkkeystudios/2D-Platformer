using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyActivationAI : MonoBehaviour
{
    public Transform player;
    public Transform homePos;
    private IAstarAI ai;
    private bool playerInRange = false; 
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && player!= null && ai != null)
        {
            ai.destination = player.position;
            ai.SearchPath();
        }
        else
        {
            ai.destination = homePos.position;
        }
    }

    public void ActivateEnemy(bool activate)
    {
       playerInRange = activate;
        if (!playerInRange)
        {
            // When deactivated, set destination back to home position
            ai.destination = homePos.position;
            ai.SearchPath();
        }
    }
}
