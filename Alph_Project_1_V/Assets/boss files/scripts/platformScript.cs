using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour
{
    public GameObject platform;
    float moveSpeed = 2.0f;
    bool moveRight = true;
    void Update()
    {
        if (platform.transform.position.x > 7 )
        {
            moveRight = false;
        }
        if (platform.transform.position.x < 0 )
        {
            moveRight = true;
        }
        if (moveRight)
            platform.transform.position = new Vector2(platform.transform.position.x + moveSpeed * Time.deltaTime, platform.transform.position.y);
        else
            platform.transform.position = new Vector2(platform.transform.position.x - moveSpeed * Time.deltaTime, platform.transform.position.y);
        
    }
}
