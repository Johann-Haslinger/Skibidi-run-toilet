using UnityEngine;

public class RainDrop : MonoBehaviour
{
    private float fallSpeed;
    private RectTransform rainArea;

    // Setup-Methode, um die Geschwindigkeit und den Bereich für den Tropfen zu setzen
    public void Setup(float speed, RectTransform area)
    {
        fallSpeed = speed;
        rainArea = area;
    }

    private void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        // Bewege den Tropfen nach unten
        rectTransform.anchoredPosition += new Vector2(0, -fallSpeed * Time.deltaTime);

        // Überprüfe, ob der Tropfen den Bildschirm verlassen hat (außerhalb von rainArea)
        if (rectTransform.anchoredPosition.y < -rainArea.rect.height / 2)
        {
            // Zerstöre den Tropfen, wenn er den unteren Rand des Panels verlässt
            Destroy(gameObject);
        }
    }
}
