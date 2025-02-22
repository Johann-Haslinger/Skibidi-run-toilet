using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip collisionSound; // Der Ton, der abgespielt wird


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            CoinManager.instance.AddScore(coinValue); 
            Destroy(gameObject); 
        }
    }
}
