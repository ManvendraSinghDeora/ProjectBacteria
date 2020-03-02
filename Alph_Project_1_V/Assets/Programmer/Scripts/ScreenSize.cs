using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
    public GameObject background;

    void Start()
    {
        background.transform.localScale = new Vector2(Camera.main.transform.localScale.x, Camera.main.transform.localScale.y);
    }

    void Update()
    {
        
    }
}
