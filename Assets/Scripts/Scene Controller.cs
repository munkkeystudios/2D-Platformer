using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void OnEnable()
     {
         SceneManager.sceneLoaded += OnSceneLoaded;
     }

     void OnDisable()
     {
         SceneManager.sceneLoaded -= OnSceneLoaded;
     }

     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
     {
         // Assuming you have a way to access the player's Health component here
         Health playerHealth = FindObjectOfType<Health>();
         if (playerHealth != null)
         {
             // This will force the health bar to update
             playerHealth.TriggerHealthChangedEvent();
         }
     }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
   
    
   
}
