using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveBullets : MonoBehaviour
{

    public float moveSpeed = 5f;
    Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (waveShooting.instance.random == 0)
        {
            rb.velocity = new Vector2(0, -moveSpeed);
        }
        else if (waveShooting.instance.random == 1)
        {
            rb.velocity = new Vector2(-moveSpeed,0);
        }
        else if(waveShooting.instance.random == 2)
        {
            rb.velocity = new Vector2(moveSpeed,0);
        }
        else if(waveShooting.instance.random == 3)
        {
            rb.velocity = new Vector2(0, moveSpeed);
        }
        Destroy(gameObject, 8);
    }
}
