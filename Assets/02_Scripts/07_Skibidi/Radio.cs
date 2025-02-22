using UnityEngine;

public class ShakeAndScale : MonoBehaviour
{
    public float shakeAmount = 10f; // Maximale Verschiebung in Pixeln
    public float shakeSpeed = 10f;  // Geschwindigkeit der Schüttelbewegung
    public float scaleAmount = 0.1f; // Maximale Skalierung
    public float scaleSpeed = 1f;    // Geschwindigkeit der Skalierung

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Vector3 originalScale;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;
        originalScale = rectTransform.localScale;
    }

    void Update()
    {
        // Schüttelbewegung
        float shakeX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) * shakeAmount * 2 - shakeAmount;
        float shakeY = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) * shakeAmount * 2 - shakeAmount;
        rectTransform.localPosition = originalPosition + new Vector3(shakeX, shakeY, 0f);

        // Skalierung
        float scale = Mathf.PingPong(Time.time * scaleSpeed, scaleAmount) + 1f;
        rectTransform.localScale = originalScale * scale;
    }
}
