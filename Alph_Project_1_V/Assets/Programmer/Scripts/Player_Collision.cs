using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    Currency currency;
    private void Awake()
    {
        currency = GetComponent<Currency>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "mineralCurr1")
        {
            currency.mineralCurr1 = currency.mineralCurr1 + 1;
        }
        if(collision.collider.name == "mineralCurr")
        {
            currency.mineralCurr2 = currency.mineralCurr1 + 1;
        }
    }
}
