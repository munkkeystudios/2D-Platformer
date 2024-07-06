using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boss_movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float x_distance;
    //public float reset_trigger_distance;
    //public float max_chase_distance;
    [SerializeField] private float monster_scale_x;
    [SerializeField] private float monster_scale_y;
    //public float idle_slowdown;

    //private Animator anim;

    public bool chase_player;


    //public GameObject reset_point;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if ( player == null)
        {
            Debug.LogError("One or more required components not assigned in" + gameObject.name);
        }

        chase_player = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = (Mathf.Abs(player.transform.position.x - transform.position.x));
        //float distanceToResetPoint = Mathf.Abs(reset_point.transform.position.x - transform.position.x);

        if (chase_player)
        {
            //anim.SetBool("is_moving", true);

            if (transform.position.x > player.transform.position.x)
            {
                transform.localScale = new Vector2(monster_scale_x, monster_scale_y);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x < player.transform.position.x)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.localScale = new Vector2(-1 * monster_scale_x, monster_scale_y);
            }
        }
        else
        {

            if (x_distance > distanceToPlayer)
            {
                chase_player = true;
            }
            else
            {
                chase_player = false;
                //anim.SetBool("is_moving", false);
            }
                


        }


    }
}
