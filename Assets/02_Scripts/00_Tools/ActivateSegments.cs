using System.Collections.Generic;
using UnityEngine;

public class ActivateSegments : MonoBehaviour
{
    [SerializeField] private List<GameObject> _segmentsToAdd;
    private void Start()
    {
        World.Instance.AddPossibleSegments(_segmentsToAdd);
        Destroy(gameObject);
    }

   
}
