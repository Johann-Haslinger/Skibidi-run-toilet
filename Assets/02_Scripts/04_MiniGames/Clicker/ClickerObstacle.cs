using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ClickerObstacle : MonoBehaviour
{
    [SerializeField] private List<Color> _colors;
    [SerializeField] private AudioClip _clickSound;
    
    private int _currentClicks;
    private SpriteRenderer _sR;

    private void Start()
    {
        _sR = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Click();
        if (_currentClicks == _colors.Count - 1)
        {
            Finish();
        }
    }

    private void Finish()
    {
        Destroy(gameObject);
    }

    private void Click()
    {
        CameraShaker.Instance.CustomShake(0.1f, new Vector3(0.1f, 0.1f));
        AudioSource.PlayClipAtPoint(_clickSound, Vector3.zero);
        _currentClicks++;
        _sR.color = _colors[_currentClicks];
        
    }
}
