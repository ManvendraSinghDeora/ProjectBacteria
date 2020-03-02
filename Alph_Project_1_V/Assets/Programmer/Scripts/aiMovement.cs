using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiMovement : MonoBehaviour
{
    //[SerializeField] private fieldOfView fieldOfView;

    public Animator anim;
    public float speed,distance;
    public bool movingRight;
    public Transform groundDetection;
    Vector3 startPos;
    public bool patrolling=true;
    GameObject player;
    public static aiMovement instanceinstance;
    Rigidbody2D rb;
    public float attackRange = 1.5f;
    [SerializeField]
    private int Health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(Health<=0)
        {
            Destroy(gameObject);
        }
        if(patrolling)
        {
            //transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            //if(transform.position==startPos)
            //{
                patrol();
            //}
        }
        else if(patrolling==false)
        {
            //move to player
           transform.position= Vector2.MoveTowards(rb.position, player.transform.position, speed*Time.deltaTime*1.5f);
        }
        //attack
        if (Vector2.Distance(player.transform.position, rb.position) <= attackRange)
        {
            anim.SetBool("attack", true);
        }
        else
        {
            anim.SetBool("attack", false);
        }
    }
    public void patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            //player health damage
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Health--;
            //1 heart lost
        }
        if (collision.collider.CompareTag("spit"))
        {
            Health--;
        }
    }
}
