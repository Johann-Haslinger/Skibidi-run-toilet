using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public UnityEvent onPlayerCollision; // Event für externe Aktionen

    private void Awake()
    {
        if (onPlayerCollision == null)
            onPlayerCollision = new UnityEvent(); // Verhindert Null-Referenz
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPlayerCollision?.Invoke(); // Event auslösen
        }
    }
}
