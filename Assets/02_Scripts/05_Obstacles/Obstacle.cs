using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{
    public UnityEvent onPlayerTrigger; // Event für externe Aktionen

    private void Awake()
    {
        if (onPlayerTrigger == null)
            onPlayerTrigger = new UnityEvent(); // Verhindert Null-Referenz
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlayerTrigger?.Invoke(); // Event auslösen
        }
    }
}
