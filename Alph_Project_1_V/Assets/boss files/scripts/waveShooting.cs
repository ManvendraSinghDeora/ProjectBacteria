using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveShooting : MonoBehaviour
{
    public Transform [] northSpawn, eastSpawn, southSpawn, westSpawn;
    [SerializeField] GameObject bullet;
    public static waveShooting instance;

    public int random;
    public float fireRate;
    float nextFire;

    private void Start()
    {
        instance = this;
        nextFire = Time.time;
    }

   
    private void Update()
    {
        waveShot();
    }

    void waveShot()
    {
        random = Random.Range(0, 4);
        if(Time.time>nextFire)
        {
            if(random==0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Instantiate(bullet, northSpawn[i].transform.position, Quaternion.identity);
                    nextFire = Time.time + fireRate;
                }
            }
           else if (random == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Instantiate(bullet, eastSpawn[i].transform.position, Quaternion.identity);
                    nextFire = Time.time + fireRate;
                }
            }
           else if (random == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Instantiate(bullet, westSpawn[i].transform.position, Quaternion.identity);
                    nextFire = Time.time + fireRate;
                }
            }
            else if (random == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    Instantiate(bullet, southSpawn[i].transform.position, Quaternion.identity);
                    nextFire = Time.time + fireRate;
                }
            }

        }
    }
}
