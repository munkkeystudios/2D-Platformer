using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutscene : MonoBehaviour
{

    [SerializeField] private GameObject maincam;
    [SerializeField] private GameObject custscenecam;
    // Start is called before the first frame update
    void Start()
    {
        if (maincam == null)
        {
            Debug.LogError("maincam not assigned");
            return;
        }
        else if (custscenecam == null)
        {
            Debug.LogError("cutscenecam not assigned");
            return;
        }

        custscenecam.SetActive(true);
        maincam.SetActive(false);
        Invoke("switchback", 73f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switchback();
        }
    }

    void switchback()
    {
        custscenecam.SetActive(false);
        maincam.SetActive(true);
    }
}
