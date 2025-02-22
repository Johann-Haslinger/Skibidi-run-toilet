using System;
using TMPro;
using UnityEngine;

public class AnswerBlock : MonoBehaviour
{
    [SerializeField] private Quiz _quiz;
    [SerializeField] private int _number;
    [SerializeField] private TMP_Text _text;

    public void Setup(string text)
    {
        _text.text = text;
    }
    
    private void OnMouseDown()
    {
        _quiz.AnswerSubmitted(_number);
    }

    public void CorrectAnswer()
    {
        
    }

    public void WrongAnswer()
    {
        
    }
}
