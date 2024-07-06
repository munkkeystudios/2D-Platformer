using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {get; private set; }

    [SerializeField]private TextMeshProUGUI coinText;//no. of coins
    [SerializeField] private int coins =0;

    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (coinText == null)
        {

           Debug.LogError("CoinManager: Missing reference");
        }
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();
    }
    public bool SpendCoins(int amount)
    {
        if(coins>= amount)
        {
            coins -= amount;
            UpdateCoinText();
            return true;
        }
        else
        {
            Debug.Log("Not enough coins");
            return false;
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    private void UpdateCoinText()
    {

        if(coinText !=null )
        {
            coinText.text = coins.ToString();
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the new coinText UI element by tag
        GameObject coinTextObj = GameObject.FindWithTag("CoinAmount");
        if (coinTextObj != null)
        {
            coinText = coinTextObj.GetComponent<TextMeshProUGUI>();
            UpdateCoinText(); // Update the coin count in the new UI element
        }
        else
        {
            Debug.LogError("CoinManager: Failed to find CoinText in the new scene.");
        }
    }
}
