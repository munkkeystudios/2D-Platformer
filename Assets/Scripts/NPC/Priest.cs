using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    private bool played = false;

    private bool facingRight = true;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject maincam;
    [SerializeField] private GameObject custscenecam;

    private Animator anim;
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not assigned to priest");
            return;
        }
        else if (player == null)
        {
            Debug.LogError("player not assigned to priest");
            return;
        }
        else if (maincam == null)
        {
            Debug.LogError("maincam not assigned");
            return;
        }
        else if (custscenecam == null)
        {
            Debug.LogError("cutscenecam not assigned");
            return;
        }

        custscenecam.SetActive(false);
        maincam.SetActive(true);

        ActivateLayer("Idle Layer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            Invoke("Play", 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
        if (player.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    private void ActivateLayer(string layerName)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            {
                anim.SetLayerWeight(i, 0);

            }


            anim.SetLayerWeight(anim.GetLayerIndex(layerName), 1);
        }
    }

    void Play()
    {
        played = true;
        maincam.SetActive(false);
        custscenecam.SetActive(true);
        Invoke("switchBack", 6f);
    }

    void switchBack()
    {
        custscenecam.SetActive(false);
        maincam.SetActive(true);
    }
    void Flip()
    {
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;

        facingRight = !facingRight;
    }
}
