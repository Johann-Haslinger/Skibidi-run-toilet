using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private List<GameObject> _counters;
    [SerializeField] private int _neededCount = 0;
    
    private int _currentCounter = 0;
    private void Start()
    {
        _coin.SetActive(false);
    }

    public void Click(int number)
    {
        if (number == _currentCounter + 1)
        {
            _currentCounter++;
        }
        else
        {
            Fail();
        }

        if (_currentCounter == _neededCount)
        {
            _coin.SetActive(true);
            Destroy(this);
        }
       
    }

    private void Fail()
    {
        Destroy(gameObject);
    }
}
