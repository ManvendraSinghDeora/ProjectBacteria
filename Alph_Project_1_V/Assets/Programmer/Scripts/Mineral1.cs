using UnityEngine;

public class Mineral1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
