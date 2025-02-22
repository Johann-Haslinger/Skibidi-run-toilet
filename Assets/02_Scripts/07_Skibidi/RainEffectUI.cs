using UnityEngine;
using System.Collections;

public class RainEffectUI : MonoBehaviour
{
    public GameObject raindropPrefab;  // Das Prefab für den Regentropfen
    public int numberOfRaindrops = 100; // Anzahl der Regentropfen pro Regenfall
    public float rainSpeed = 2f;        // Geschwindigkeit, mit der die Tropfen fallen
    public AudioClip rainSound;         // Der Regen-Sound, den du im Inspektor zuweisen kannst

    private RectTransform canvasRect;   // Referenz auf das Canvas-RectTransform
    private AudioSource audioSource;    // AudioSource zum Abspielen des Sounds
    private bool isRaining = false;     // Überwacht, ob es gerade regnet

    private void Start()
    {
        // Zugriff auf das Canvas, falls es sich in einem Parent befindet
        canvasRect = GetComponentInParent<Canvas>()?.GetComponent<RectTransform>();

        if (canvasRect == null)
        {
            Debug.LogError("Kein Canvas im Parent gefunden!");
            return;
        }

        audioSource = GetComponent<AudioSource>();  // Hole die AudioSource-Komponente
        StartCoroutine(StartRainFalls());
    }

    private IEnumerator StartRainFalls()
    {
        while (true)
        {
            if (!isRaining)
            {
                // Starte den Regen nur, wenn gerade kein Regen stattfindet
                StartCoroutine(GenerateRain());
                // Spiele den Regen-Sound ab
                PlayRainSound();
                isRaining = true;

                // Warte 30 Sekunden bis zum nächsten Regenfall
                yield return new WaitForSeconds(30f);

                // Stoppe den Regen-Sound nach der Pause
                StopRainSound();
                isRaining = false;
            }
            else
            {
                yield return null;  // Warte auf den nächsten Update-Zyklus
            }
        }
    }

    private IEnumerator GenerateRain()
    {
        // Bestimme die Positionen innerhalb des Canvas (z. B. Bildschirmbreite)
        float minX = -canvasRect.rect.width / 2;
        float maxX = canvasRect.rect.width / 2;

        Debug.Log("Regen beginnt mit " + numberOfRaindrops + " Tropfen!");

        for (int i = 0; i < numberOfRaindrops; i++)
        {
            // Erstelle einen Regentropfen an einer zufälligen Position innerhalb des Canvas
            Vector2 spawnPosition = new Vector2(
                Random.Range(minX, maxX), 
                canvasRect.rect.height / 2);  // Startposition ist oben im Canvas

            // Instanziiere den Regentropfen zur Laufzeit
            GameObject raindrop = Instantiate(raindropPrefab, spawnPosition, Quaternion.identity);
            raindrop.transform.SetParent(canvasRect, false);  // Setze den Parent nur zur Laufzeit

            // Lasse den Tropfen fallen und übergebe das Canvas
            RainDrop raindropScript = raindrop.GetComponent<RainDrop>();
            raindropScript.Setup(rainSpeed, canvasRect);

            // Warte einen kleinen Moment, bevor der nächste Tropfen erzeugt wird
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("Regen ist vorbei!");

        // Stoppe den Sound direkt nach dem Ende des Regenfalls
        StopRainSound();
    }

    private void PlayRainSound()
    {
        if (audioSource != null && rainSound != null)
        {
            audioSource.clip = rainSound;
            audioSource.loop = true; // Setze den Sound in eine Schleife
            audioSource.Play();
        }
    }

    private void StopRainSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
