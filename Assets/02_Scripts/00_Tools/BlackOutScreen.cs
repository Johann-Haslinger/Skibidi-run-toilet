using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlackOutScreen : MonoBehaviour
{
    [SerializeField] private RawImage _blackoutImage;
    public void Blackout(float duration)
    {
        _blackoutImage.DOFade(1, duration);
    }
}
