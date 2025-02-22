using UnityEngine;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour
{
  [System.Serializable]
  public class ParallaxLayer
  {
    public GameObject prefab;
    public int numberOfCopies = 2;
    public float layerSpeed = 1f;
    public float zPosition = 0f; // Neue Variable für Z-Position
    public List<GameObject> copies = new List<GameObject>();
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
      float width = layer.prefab.GetComponent<SpriteRenderer>().bounds.size.x;

      for (int j = 0; j < layer.numberOfCopies; j++)
      {
        GameObject copy = Instantiate(layer.prefab, 
          new Vector3(width * j, 0, layer.zPosition), // Multiplied width by 2
          Quaternion.identity);
        copy.transform.parent = layer.prefab.transform.parent;
        layer.copies.Add(copy);
      }
    }
  }

  private void Update()
  {
    for (int i = 0; i < parallaxLayers.Count; i++)
    {
      ParallaxLayer layer = parallaxLayers[i];
      float speed = baseSpeed * layer.layerSpeed;
      float width = layer.prefab.GetComponent<SpriteRenderer>().bounds.size.x;

      for (int j = 0; j < layer.copies.Count; j++)
      {
        GameObject copy = layer.copies[j];
        Vector3 position = copy.transform.position;
        position.x -= speed * Time.deltaTime;

        if (position.x <= -width)
        {
          float rightmostX = -float.MaxValue;
          int rightmostIndex = 0;

          for (int k = 0; k < layer.copies.Count; k++)
          {
            float x = layer.copies[k].transform.position.x;
            if (x > rightmostX)
            {
              rightmostX = x;
              rightmostIndex = k;
            }
          }

          position.x = rightmostX + width;
        }

        copy.transform.position = position;
      }
    }
  }
}
