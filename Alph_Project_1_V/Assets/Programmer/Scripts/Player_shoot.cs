using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shoot : MonoBehaviour
{
    public GameObject SpitObj;
    public Transform SpitPoint;
    public float spitForce;
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, (Input.mousePosition.z) - Camera.main.transform.position.z));

        Vector3 difference = mousePosition - transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            Spitting(direction,0f);
        }

    }
    void Spitting(Vector2 direction, float rotationZ)
    {
        GameObject spit = Instantiate(SpitObj) as GameObject;
        spit.transform.position = SpitPoint.transform.position;
        spit.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        spit.GetComponent<Rigidbody2D>().velocity = direction * spitForce;
    }
}