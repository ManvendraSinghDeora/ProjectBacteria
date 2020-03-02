using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    int value = 1;
    public CurrencyManager currencyManager;

    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currencyManager.MoneyToAdd(value);
            Destroy(gameObject);
        }
    }

}
