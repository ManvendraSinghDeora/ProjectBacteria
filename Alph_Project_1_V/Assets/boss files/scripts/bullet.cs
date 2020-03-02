using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float moveSpeed = 7f;
    Rigidbody2D rb;

    PlayerController player;
    Vector2 moveDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerController>();
        moveDir = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 4);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //damage to player
        }
    }
}
