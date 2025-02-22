using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private QuizData _quizData;
    [SerializeField] private List<AnswerBlock> _answerBlocks;
    [SerializeField] private List<Coin> _coins;
    [SerializeField] private GameObject _coinHolder;
    [SerializeField] private GameObject _coinHolderTarget;

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        _title.text = _quizData.Question;
        for (int i = 0; i < _answerBlocks.Count; i++)
        {
            _answerBlocks[i].Setup(_quizData.Answers[i]);    
        }
    }

    public void AnswerSubmitted(int number)
    {
        if (_quizData.CorrectAnswerIndex == number)
        {
            QuizSuccess(number);
        }
        else
        {
            QuizFail(number);
        }
    }

    private void QuizSuccess(int numClicked)
    {
        _answerBlocks[numClicked].CorrectAnswer();
        _coinHolder.transform.DOMove(_coinHolderTarget.transform.position, 0.15f);
    }

    private void QuizFail(int numClicked)
    {
        _answerBlocks[numClicked].WrongAnswer();
        foreach (var coin in _coins)
        {
            coin.DestroyCoin();
        }
    }
}
