using UnityEngine;
using System.Collections;

public class FlyingJeldrik : MonoBehaviour
{
    public RectTransform imageRect; // Das RectTransform des Bildes
    public float speed = 500f; // Geschwindigkeit des Bildes
    public AudioClip collisionSound; // Der Ton, der abgespielt wird
    public float shrinkDuration = 0.1f; // Dauer des Schrumpfens
    public float shrinkScale = 0.8f; // Schrumpfmaßstab

    private Vector2 direction; // Richtung, in die sich das Bild bewegt
    private float minX, maxX, minY, maxY;
    private AudioSource audioSource;
    private Vector2 originalSize;

    void Start()
    {
        if (imageRect == null)
        {
            Debug.LogError("imageRect wurde nicht zugewiesen!");
            return;
        }

        RectTransform canvasRect = imageRect.GetComponentInParent<Canvas>()?.GetComponent<RectTransform>();

        if (canvasRect == null)
        {
            Debug.LogError("Canvas konnte nicht gefunden werden!");
            return;
        }

        minX = -canvasRect.rect.width / 2f;
        maxX = canvasRect.rect.width / 2f - imageRect.rect.width;
        minY = -canvasRect.rect.height / 2f;
        maxY = canvasRect.rect.height / 2f - imageRect.rect.height;

        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // AudioSource hinzufügen, falls noch nicht vorhanden
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        originalSize = imageRect.sizeDelta;
    }

    void Update()
    {
        // Berechne die neue Position basierend auf Geschwindigkeit und Richtung
        Vector2 currentPosition = imageRect.anchoredPosition;
        currentPosition += direction * speed * Time.deltaTime;

        // Überprüfe, ob das Bild die Ränder des Canvas erreicht hat und kehre die Richtung um
        if (currentPosition.x < minX || currentPosition.x > maxX)
        {
            direction.x = -direction.x;
            currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);

            // Kollision mit der Wand, Coin hinzufügen und Ton abspielen
            OnCollisionWithWall();
        }
        if (currentPosition.y < minY || currentPosition.y > maxY)
        {
            direction.y = -direction.y;
            currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

            // Kollision mit der Wand, Coin hinzufügen und Ton abspielen
            OnCollisionWithWall();
        }

        // Setze die neue Position des Bildes
        imageRect.anchoredPosition = currentPosition;
    }

    // Methode, die bei Kollision mit der Wand aufgerufen wird
    void OnCollisionWithWall()
    {
        CoinManager.instance.AddScore(1);

        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound, 0.3f); // Kollisionston abspielen
        }

        // Start the shrinking coroutine
        StartCoroutine(ShrinkAndGrow());
    }

    // Coroutine to handle shrinking and growing effect
    IEnumerator ShrinkAndGrow()
    {
        imageRect.sizeDelta = originalSize * shrinkScale;
        yield return new WaitForSeconds(shrinkDuration);
        imageRect.sizeDelta = originalSize;
    }
}
