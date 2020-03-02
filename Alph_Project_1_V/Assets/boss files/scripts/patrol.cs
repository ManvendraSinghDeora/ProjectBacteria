using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    public float speed,startwaittime;
    float waittime;

    [SerializeField]
    int Health;
    public Transform[] movespot;
    int randomspot;

    // Start is called before the first frame update
    void Start()
    {
        waittime = startwaittime;
        randomspot = Random.Range(0, movespot.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, movespot[randomspot].position, 
            speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movespot[randomspot].position) < 0.1f)
        {
            if(waittime <= 0)
            {
                randomspot = Random.Range(0, movespot.Length);
                waittime = startwaittime;
            }
            else
            {
                waittime -= Time.deltaTime;
            }
        } 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //player health damage            
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Health--;

            //1 heart lost
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spit"))
        {
            Health--;
        }
    }
}
