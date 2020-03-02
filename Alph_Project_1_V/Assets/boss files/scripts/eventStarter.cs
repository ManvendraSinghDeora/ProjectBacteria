using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventStarter : MonoBehaviour
{
    [SerializeField]
    GameObject Boss, cam, Blocker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Boss.SetActive(true);
            Blocker.SetActive(true);
            cam.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

}
