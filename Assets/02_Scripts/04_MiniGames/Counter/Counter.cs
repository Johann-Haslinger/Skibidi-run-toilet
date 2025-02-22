using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private List<GameObject> _counters;
    [SerializeField] private int _neededCount = 0;
    [SerializeField] private Color _rightColor;
    [SerializeField] private Color _wrongColor;
    
    private int _currentCounter = 0;
    private void Start()
    {
        _coin.SetActive(false);
    }

    public void Click(GameObject obj)
    {
        if (obj == _counters[_currentCounter])
        {
            CorrectClickVFX(obj);
            _currentCounter++;
        }
        else
        {
            Fail();
            return;
        }

        if (_currentCounter == _neededCount)
        {
            Finish();
        }
    }

    private void Finish()
    {
        _coin.SetActive(true);
    }

    private void CorrectClickVFX(GameObject obj)
    {
        var sR = obj.GetComponent<SpriteRenderer>();
        sR.color = _rightColor;
        obj.transform.DOScale(Vector3.one * 1.2f, 0.15f).OnComplete(() =>
        {
            obj.transform.DOScale(0, 0.25f);
           
            sR.DOFade(0, 0.25f);
            obj.SetActive(false);
        });
    }

    private void Fail()
    {
        foreach (var counter in _counters.Where(counter => counter.activeSelf))
        {
            counter.GetComponent<SpriteRenderer>().color = _wrongColor;
            counter.transform.DOScale(Vector3.one * 1.5f, 0.2f).OnComplete(() =>
            {
                counter.transform.DOScale(Vector3.zero, 0.1f);
            });
        }
    }
}
