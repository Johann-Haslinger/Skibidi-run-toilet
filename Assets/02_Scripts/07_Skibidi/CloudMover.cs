using UnityEngine;
using UnityEngine.UI;

public class CloudMover : MonoBehaviour
{
    public RectTransform cloudUI; // UI-Element der Wolke
    public float speed = 50f; // Geschwindigkeit der Bewegung
    public float scaleSpeed = 1f; // Geschwindigkeit des Pulsierens
    public float scaleAmount = 0.1f; // Wie stark die Wolke pulsiert
    public float boundaryOffset = 100f; // Abstand vom Bildschirmrand

    private float direction = 1f; // Bewegungsrichtung (1 = rechts, -1 = links)
    private float startScale;
    private float timeOffset;

    private void Start()
    {
        startScale = cloudUI.localScale.x;
        timeOffset = Random.Range(0f, 100f); // Zufälliger Startzeitpunkt für fließendes Pulsieren

        // Starte zufällig links oder rechts
        if (Random.value > 0.5f)
            direction = -1f;
    }

    private void Update()
    {
        MoveCloud();
        PulseCloud();
    }

    private void MoveCloud()
    {
        cloudUI.anchoredPosition += new Vector2(speed * direction * Time.deltaTime, 0);

        // Bildschirmrand-Check (Canvas muss auf Screen Space Overlay sein!)
        float screenWidth = Screen.width / 2f; // UI arbeitet mit Anchors, daher Hälfte nehmen

        if (cloudUI.anchoredPosition.x > screenWidth - boundaryOffset)
        {
            direction = -1f; // Richtung ändern (nach links)
        }
        else if (cloudUI.anchoredPosition.x < -screenWidth + boundaryOffset)
        {
            direction = 1f; // Richtung ändern (nach rechts)
        }
    }

    private void PulseCloud()
    {
      float scale = startScale + Mathf.Sin((Time.time + timeOffset) * scaleSpeed) * scaleAmount;
      float largerScale = scale * 1.5f; // Vergrößere die Wolke um das 1.5-fache
      cloudUI.localScale = new Vector3(largerScale, largerScale, 1f);
    }
}
