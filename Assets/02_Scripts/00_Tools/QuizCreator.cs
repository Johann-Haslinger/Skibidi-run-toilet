using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class QuizCreator : MonoBehaviour
{
    [SerializeField] private string _text;
    
    [SerializeField] private string _name;

    [ContextMenu("Create")]
    public void CreateFromList()
    {
        List<string> lines = _text.Split("|", StringSplitOptions.None).ToList();
        int counter = 0;
        foreach (var line in lines)
        {
            CreateMyAsset(line, counter);
            counter++;
        }
    }
    
   
    public void CreateMyAsset(string text, int count)
    {
        QuizData asset = ScriptableObject.CreateInstance<QuizData>();
        
        ParseTextIntoAsset(text, asset);
        
        string fileName = AssetDatabase.GenerateUniqueAssetPath("Assets/05_Data/01_Quizzes/" + _name + count.ToString() +  ".asset");
        AssetDatabase.CreateAsset(asset, fileName);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        //Selection.activeObject = asset;
    }

    public QuizData ParseTextIntoAsset(string inputText, QuizData asset)
    {
        var lines = inputText.Split(";", StringSplitOptions.None).ToList();
        asset.Question = lines[0];
        asset.Answers.AddRange(new List<string>()
        {
            lines[1],
            lines[2],
            lines[3],
            lines[4]
        });
        asset.CorrectAnswerIndex = int.Parse(lines[5]);
        return asset;
    }
}
