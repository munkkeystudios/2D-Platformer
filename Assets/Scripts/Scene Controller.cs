using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    


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
        // Find the Health component of the player in the new scene
        Health playerHealth = FindObjectOfType<Health>();
        if (playerHealth != null)
        {
            // Force the health bar to update
            playerHealth.TriggerHealthChangedEvent();
        }

        // New addition: Directly update the HealthBarUpdater component
        HealthBarUpdater healthBarUpdater = FindObjectOfType<HealthBarUpdater>();
        if (healthBarUpdater != null && playerHealth != null)
        {
            // Ensure the HealthBarUpdater is aware of the current health component
            healthBarUpdater.SetHealthComponent(playerHealth);
            // Manually trigger an update to the health bar UI
            healthBarUpdater.UpdateHealthBar(playerHealth.CurrentHealth);
        }
    }

    public void NextLevel()
    {
        Debug.Log("NextLevel called");
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }


}
