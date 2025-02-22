using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip collisionSound; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            // Sound abspielen
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);

            // Punkte hinzufügen
            CoinManager.instance.AddScore(coinValue); 
            
            // Objekt zerstören
            Destroy(gameObject); 
        }
    }
}
