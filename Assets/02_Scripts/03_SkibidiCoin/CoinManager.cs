using UnityEngine;
using TMPro;
using System;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    // Event für Änderungen am Score
    public event Action OnScoreChanged;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        OnScoreChanged?.Invoke(); // Event auslösen
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skibidi: " + score;
        }
    }
}
