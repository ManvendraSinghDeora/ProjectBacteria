using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral2 : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
