using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for three attack types, not in use currently
public class ColliderDetection : MonoBehaviour
{
    [SerializeField] private float damage;

    private void Start()
    {
        GetComponent<PolygonCollider2D>().enabled = false;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health enemyHealth = collision.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
