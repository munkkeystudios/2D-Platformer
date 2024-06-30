using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    private float timer = 0.0f;
    [SerializeField] private float attackAnimationDelay = 4.0f;


    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //To not play the attack animation continously i have introduced a time based loop

        timer += Time.deltaTime;
        if (timer > attackAnimationDelay)
        {
            timer = 0.0f; 
            anim.SetBool("Attack", true);
        }

    }

    IEnumerator SetAttackAnimation()
    {
        Debug.Log("setting");
        yield return new WaitForSeconds(1);
        StartCoroutine(ResetAttackAnimation());
    }
    IEnumerator ResetAttackAnimation()
    {
        Debug.Log("resetting");
        yield return new WaitForSeconds(1);
        anim.SetBool("Idle", true);
    }

    }
