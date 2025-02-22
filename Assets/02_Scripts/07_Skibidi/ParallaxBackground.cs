using UnityEngine;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour
{
  [System.Serializable]
  public class ParallaxLayer
  {
    public SpriteRenderer backgroundSprite;
    public GameObject prefab; // Neues Feld für Prefab
    public float scrollSpeed = 1f;
    public int numberOfCopies = 2;
    public List<SpriteRenderer> copies = new List<SpriteRenderer>();
  }

  public List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();
  public float baseSpeed = 1f;

  private void Start()
  {
    InitializeLayers();
  }

  private void InitializeLayers()
  {
    for (int i = 0; i < parallaxLayers.Count; i++)
    {
      ParallaxLayer layer = parallaxLayers[i];
      
      // Bestimme das Ursprungsobjekt (entweder Sprite oder Prefab)
      SpriteRenderer sourceSprite;
      if (layer.prefab != null)
      {
        // Wenn ein Prefab gegeben ist, instantiiere es und nutze seinen SpriteRenderer
        GameObject firstInstance = Instantiate(layer.prefab, transform);
        sourceSprite = firstInstance.GetComponent<SpriteRenderer>();
        layer.backgroundSprite = sourceSprite;
      }
      else
      {
        sourceSprite = layer.backgroundSprite;
      }

      if (sourceSprite == null)
      {
        Debug.LogError("Keine Sprite-Quelle gefunden für Layer " + i);
        continue;
      }

      float width = sourceSprite.bounds.size.x;

      // Create copies with exact positioning
      for (int j = 1; j < layer.numberOfCopies; j++)
      {
        SpriteRenderer copy;
        if (layer.prefab != null)
        {
          GameObject instance = Instantiate(layer.prefab, transform);
          copy = instance.GetComponent<SpriteRenderer>();
        }
        else
        {
          copy = Instantiate(sourceSprite, sourceSprite.transform.parent);
        }
        
        copy.transform.position = new Vector3(width * j, 0, sourceSprite.transform.position.z);
        layer.copies.Add(copy);
      }

      layer.copies.Insert(0, sourceSprite);
    }
  }

  // Update-Methode bleibt unverändert
  private void Update()
  {
    // ... bestehender Update-Code ...
  }
}
