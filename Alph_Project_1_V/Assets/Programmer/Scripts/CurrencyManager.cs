using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public Text moneyText;
    public int currentGold;

    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentMoney"))
        {
            currentGold = PlayerPrefs.GetInt("CurrentMoney");
        }
        else
        {
            currentGold = 0;
            PlayerPrefs.SetInt("CurrentMoney", 0);
        }

        moneyText.text = "Currency : " + currentGold;

    }

    public void MoneyToAdd(int value)
    {
        currentGold += value;
        Debug.Log(currentGold);
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Currency : " + currentGold;
    }

    void Update()
    {

        moneyText.text = "Currency : " + currentGold;
    }
}
