using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_based_enemy : MonoBehaviour
{

    public float speed;
    public float x_distance;
    public float reset_trigger_distance;
    public float max_chase_distance;
    public float monster_scale_x;
    public float monster_scale_y;
    public float idle_slowdown;

    private Animator anim;

    public bool chase_player;
    public bool flip;



    public GameObject reset_point;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        chase_player = false;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distanceToResetPoint = Mathf.Abs(reset_point.transform.position.x - transform.position.x);

        if (chase_player == true)
        {

            if (distanceToResetPoint > max_chase_distance)
            { 
                chase_player=false;
            }

            if (transform.position.x > player.transform.position.x)
            {
                transform.localScale = new Vector3(monster_scale_x, monster_scale_y, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
                anim.SetBool("Move", true);
            }
            if (transform.position.x < player.transform.position.x)
            {
                transform.localScale = new Vector3(-1 * monster_scale_x, monster_scale_y, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
                anim.SetBool("Move", true);
            }
        }
        else
        {


            if (distanceToPlayer < x_distance)
            {
                chase_player = true;
            }
            else
            {
                if (Mathf.Abs(reset_point.transform.position.x - transform.position.x) > reset_trigger_distance)
                {
                    if (transform.position.x > reset_point.transform.position.x)
                    {
                        transform.localScale = new Vector3(monster_scale_x, monster_scale_y, 1);
                        transform.position += Vector3.left * (speed - idle_slowdown) * Time.deltaTime;
                    }
                    if (transform.position.x < reset_point.transform.position.x)
                    {
                        transform.localScale = new Vector3(-1 * monster_scale_x, monster_scale_y, 1);
                        transform.position += Vector3.right * (speed - idle_slowdown) * Time.deltaTime;
                    }
                    if (transform.position.x == reset_point.transform.position.x)
                    {

                        anim.SetBool("Move", false);
                    }

                }
            }

            }

        }


}

