using System;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Vector3 _strength;
    [SerializeField] private float _duration;

    public static CameraShaker Instance;
    private bool _shaking;

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

    public bool IsCamShaking()
    {
        return _shaking;
    }
    
    public void DefaultShake()
    {
        CustomShake(_duration, _strength);
    }

    public void CustomShake(float duration, Vector3 strength)
    {
        _shaking = true;
        var temp = transform.position;
        transform.DOShakePosition(duration, strength).OnComplete(() =>
        {
            _shaking = false;
            transform.position = temp;
        });
    }
}
