using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {get; private set; }

    [SerializeField]private TextMeshProUGUI coinText;//no. of coins
    private int coins =0;

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
}
