using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantCutscene : MonoBehaviour
{
    private bool played = false;

    [SerializeField] private GameObject maincam;
    [SerializeField] private GameObject custscenecam;

    // Start is called before the first frame update
    void Start()
    {
        if(maincam == null)
        {
            Debug.LogError("maincam not assigned");
            return;
        }
        else if(custscenecam == null)
        {
            Debug.LogError("cutscenecam not assigned");
            return;
        }

        custscenecam.SetActive(false);
        maincam.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("here");
            Invoke("Play", 1f);
        }
    }

    void Play()
    {
        played = true;
        maincam.SetActive(false);
        custscenecam.SetActive(true);
        Invoke("switchBack", 22f);
    }

    void switchBack()
    {
        custscenecam.SetActive(false);
        maincam.SetActive(true);
    }
}
