using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "Scriptable Objects/QuizData")]
public class QuizData : ScriptableObject
{
    public string Question;
    public List<string> Answers = new List<string>();
    public int CorrectAnswerIndex;
}
