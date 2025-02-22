using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UILightningBolt : MonoBehaviour
{
  public RectTransform cloudUI;  // Die Wolke als UI-Element
  public RectTransform lightningParent;  // UI-Container für den Blitz
  public Sprite lightningSprite;  // Das Blitz-Sprite
  public int maxSegments = 8;  // Maximale Segmente
  public float segmentWidth = 5f;  // Breite eines Segments
  public float segmentHeight = 20f;  // Höhe eines Segments
  public float spreadFactor = 30f;  // Wie stark sich die Blitze nach außen ausbreiten
  public float branchProbability = 0.5f;  // Wahrscheinlichkeit einer Verzweigung
  public AudioClip lightningSound;  // Soundeffekt für den Blitz
  public float minInterval = 2f;  // Minimales Intervall zwischen Blitzen
  public float maxInterval = 5f;  // Maximales Intervall zwischen Blitzen

  private List<GameObject> lightningSegments = new List<GameObject>();
  private AudioSource audioSource;

  private void Start()
  {
    audioSource = gameObject.AddComponent<AudioSource>();
    ScheduleNextLightningStrike();
  }

  private void ScheduleNextLightningStrike()
  {
    float interval = Random.Range(minInterval, maxInterval);
    Invoke("StartLightningStrike", interval);
  }

  private void StartLightningStrike()
  {
    ClearLightning();
    lightningParent.gameObject.SetActive(true);
    GenerateLightning(cloudUI.anchoredPosition, 0);
    audioSource.PlayOneShot(lightningSound, 0.1f);  // Soundeffekt abspielen
    Invoke("StopLightningStrike", 0.2f);
    ScheduleNextLightningStrike();
  }

  private void GenerateLightning(Vector2 startPos, int depth)
  {
    if (depth >= maxSegments) return;

    int branchCount = Random.value < branchProbability ? 2 : 1; // Hauptast + evtl. Nebenast

    for (int i = 0; i < branchCount; i++)
    {
      Vector2 newPos = startPos + new Vector2(Random.Range(-spreadFactor, spreadFactor), -segmentHeight);
      GameObject segment = CreateSegment(startPos, newPos);
      lightningSegments.Add(segment);

      GenerateLightning(newPos, depth + 1); // Rekursive Fortsetzung des Blitzes
    }
  }

  private GameObject CreateSegment(Vector2 start, Vector2 end)
  {
    GameObject segment = new GameObject("LightningSegment");
    segment.transform.SetParent(lightningParent);
    segment.transform.localScale = Vector3.one;

    Image img = segment.AddComponent<Image>();
    img.sprite = lightningSprite;
    img.color = Color.white;

    RectTransform rect = img.rectTransform;
    rect.sizeDelta = new Vector2(segmentWidth, Vector2.Distance(start, end));
    rect.anchoredPosition = (start + end) / 2;
    rect.rotation = Quaternion.Euler(0, 0, GetAngle(start, end));

    return segment;
  }

  private float GetAngle(Vector2 start, Vector2 end)
  {
    return Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;
  }

  private void ClearLightning()
  {
    foreach (var segment in lightningSegments)
    {
      Destroy(segment);
    }
    lightningSegments.Clear();
  }

  private void StopLightningStrike()
  {
    lightningParent.gameObject.SetActive(false);
  }
}
