using System;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Vector3 _strength;
    [SerializeField] private float _duration;

    public static CameraShaker Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void DefaultShake()
    {
        var temp = transform.position;
        transform.DOShakePosition(_duration, _strength).OnComplete(() =>
        {
            transform.position = temp;
        });
    }

    public void CustomShake(float duration, float strength)
    {
        var temp = transform.position;
        transform.DOShakePosition(duration, strength).OnComplete(() =>
        {
            transform.position = temp;
        });
    }
}
