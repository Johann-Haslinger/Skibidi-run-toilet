using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnswerBlock : MonoBehaviour
{
    [SerializeField] private Quiz _quiz;
    [SerializeField] private int _number;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _correctColor;
    [SerializeField] private Color _wrongColor;
    
    private SpriteRenderer _sR;
    private Color _baseColor;
    
    public void Setup(string text)
    {
        _text.text = text;
        _sR = GetComponent<SpriteRenderer>();
        _baseColor = _sR.color;
    }
    
    private void OnMouseDown()
    {
        _quiz.AnswerSubmitted(_number);
    }

    public void CorrectAnswer()
    {
        BlinkColor(_correctColor);
    }

    public void WrongAnswer()
    {
        BlinkColor(_wrongColor);
    }

    private void BlinkColor(Color colorToBlink)
    {
        _sR.color = colorToBlink;
        
        Sequence seq = DOTween.Sequence();
        seq.InsertCallback(0.1f, () =>
        {
            _sR.color = _baseColor;
        });
        seq.InsertCallback(0.2f, () =>
        {
            _sR.color = colorToBlink;
        });
    }
}
