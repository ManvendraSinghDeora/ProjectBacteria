 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRColor : MonoBehaviour
{
    [SerializeField] SpriteRenderer player;
    Color random_Color = Color.white;
    Color targetColor;
    float timeLeft;
    [SerializeField]float timeLeftValue;

    void Start()
    {
        player = GetComponent<SpriteRenderer>();
        targetColor = new Color(255,255,255);
    }
    void Update()
    {
        colourChange();
    }

    void colourChange()
    {
        if (timeLeft <= Time.deltaTime)
        {
            player.color = targetColor;

            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = timeLeftValue;
        }
        else
        {
            player.color = Color.Lerp(player.color, targetColor, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
        }
    }
}