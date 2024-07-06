using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{

    private bool played = false;


    [SerializeField] private GameObject player;
    [SerializeField] private GameObject maincam;
    [SerializeField] private GameObject custscenecam;


    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("player not assigned to merchant");
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

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange)
        {
            Debug.Log("collision");
            Invoke("Play", 0.5f);
        }
    }

    void Play()
    {
        played = true;
        maincam.SetActive(false);
        custscenecam.SetActive(true);
        Invoke("switchBack", 13f);
    }

    void switchBack()
    {
        custscenecam.SetActive(false);
        maincam.SetActive(true);
    }
    
}
