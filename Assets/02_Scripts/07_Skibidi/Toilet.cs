using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RandomUIPopup : MonoBehaviour
{
    public Image uiImage; // Das UI-Image, das eingeblendet wird
    public AudioSource audioSource; // Der AudioSource für den Sound
    public AudioClip popupSound; // Der Sound, der abgespielt wird
    public float minScale = 0.5f, maxScale = 1.5f; // Skalierungsbereich
    public float fadeDuration = 0.5f; // Zeit für das Ein- und Ausblenden
    public float displayTime = 1.0f; // Wie lange das Image sichtbar bleibt
    public Vector2 intervalRange = new Vector2(2f, 5f); // Min- und Max-Wartezeit zwischen Popups
    public float pulseDuration = 0.2f; // Dauer des schnellen Hochskalierens und Zurückskalierens
    
    private RectTransform canvasRect;

    private void Start()
    {
        if (uiImage == null)
        {
            Debug.LogError("UI Image is not assigned!");
            return;
        }
        
        canvasRect = uiImage.canvas.GetComponent<RectTransform>();
        uiImage.gameObject.SetActive(false);
        StartCoroutine(PopupRoutine());
    }

    private IEnumerator PopupRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(intervalRange.x, intervalRange.y);
            yield return new WaitForSeconds(waitTime);

            // Begrenzungen anhand des Canvas setzen
            float halfWidth = canvasRect.rect.width / 2;
            float halfHeight = canvasRect.rect.height / 2;
            
            Vector2 randomPosition = new Vector2(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight));
            uiImage.rectTransform.anchoredPosition = randomPosition;

            // Zufällige Skalierung setzen
            float randomScale = Random.Range(minScale, maxScale);
            uiImage.rectTransform.localScale = Vector3.one * randomScale;

            // Bild einblenden mit Animation
            uiImage.gameObject.SetActive(true);
            StartCoroutine(FadeImage(0f, 1f, fadeDuration));
            StartCoroutine(PulseEffect(randomScale));
            
            // Sound abspielen
            if (audioSource && popupSound)
            {
                audioSource.PlayOneShot(popupSound);
            }

            yield return new WaitForSeconds(displayTime);
            
            // Bild ausblenden
            StartCoroutine(FadeImage(1f, 0f, fadeDuration));
            yield return new WaitForSeconds(fadeDuration);
            uiImage.gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = uiImage.color;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            uiImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        uiImage.color = color;
    }

    private IEnumerator PulseEffect(float baseScale)
    {
        float elapsed = 0f;
        float maxPulseScale = baseScale * 1.5f;
        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(baseScale, maxPulseScale, elapsed / pulseDuration);
            uiImage.rectTransform.localScale = Vector3.one * scale;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(maxPulseScale, baseScale, elapsed / pulseDuration);
            uiImage.rectTransform.localScale = Vector3.one * scale;
            yield return null;
        }
    }
}