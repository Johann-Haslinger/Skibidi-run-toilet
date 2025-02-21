using UnityEngine;
using UnityEngine.UI;

public class BouncingImage : MonoBehaviour
{
    public RectTransform imageRect; // Das RectTransform des Bildes
    public float speed = 500f; // Geschwindigkeit des Bildes

    private Vector2 direction; // Richtung, in die sich das Bild bewegt

    private float minX, maxX, minY, maxY;

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
        }
        if (currentPosition.y < minY || currentPosition.y > maxY)
        {
            direction.y = -direction.y;
            currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
        }

        // Setze die neue Position des Bildes
        imageRect.anchoredPosition = currentPosition;
    }
}
