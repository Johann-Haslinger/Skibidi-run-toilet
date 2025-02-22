using System.Collections.Generic;
using UnityEngine;

public class ActivateSegments : MonoBehaviour
{
    [SerializeField] private List<GameObject> _segmentsToAdd;
    
    [SerializeField] private string _popTitleText;
    [SerializeField] private string _popMainText;
    private void Start()
    {
        PopUp.Instance.Setup(_popTitleText, _popMainText);
        PopUp.Instance.Show();
        
        World.Instance.AddPossibleSegments(_segmentsToAdd);
        Destroy(gameObject);
    }

   
}
