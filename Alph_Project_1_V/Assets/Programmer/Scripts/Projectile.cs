using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Update()
    {
        DestroyProjectile();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyProjectile();
    }


    void DestroyProjectile()
    {
        Destroy(gameObject,3f);
    }
}
