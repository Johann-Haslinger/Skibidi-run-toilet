using UnityEngine;

public class BackgroundSetter : MonoBehaviour
{
    public Sprite backgroundSprite; // Hier das Bild im Inspector zuweisen

    void Start()
    {
        // Neues GameObject für den Hintergrund erstellen
        GameObject bg = new GameObject("Background");
        
        // SpriteRenderer hinzufügen
        SpriteRenderer sr = bg.AddComponent<SpriteRenderer>();
        sr.sprite = backgroundSprite;
        
        // Hintergrund hinter allem anzeigen (höhere negative Z-Position)
        bg.transform.position = new Vector3(0, 0, 80);
        
        // Kamera-Breite und Höhe berechnen
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;
        
        // Größe des Sprites anpassen
        if (sr.sprite != null)
        {
            float spriteWidth = sr.sprite.bounds.size.x;
            float spriteHeight = sr.sprite.bounds.size.y;
            bg.transform.localScale = new Vector3(screenWidth / spriteWidth, screenHeight / spriteHeight, 1);
        }
    }
}
