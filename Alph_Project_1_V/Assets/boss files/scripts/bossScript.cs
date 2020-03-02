using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public GameObject bullet,player;
    GameObject abc;
    Rigidbody2D bRb;
    float bSpeed = 10;
    public float fireRate;
    float nextFire;

    public Animator anim;
    public float bossHealth = 100;

    private void Start()
    {
        nextFire = Time.time;
    }
    private void Update()
    {
        if (bossHealth <= 0)
        {
            Destroy(gameObject);
        }
        shootBullet();
        if(bossHealth<=60)
        {
            //2ND STAGE 
            anim.SetBool("2nd Stage", true);
            fireRate = 1;
            waveShooting.instance.fireRate = 4;
        }
    }
    void shootBullet()
    {
        if (Time.time > nextFire)
        {
                Instantiate(bullet,transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
        }

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("player collided");
            //damage to player            
            Destroy(abc);
        }
        else if(collision.CompareTag("wall"))
        {
            Destroy(abc);
        }
        if (collision.CompareTag("spit"))
        {
            bossHealth--;
        }
    }
}
