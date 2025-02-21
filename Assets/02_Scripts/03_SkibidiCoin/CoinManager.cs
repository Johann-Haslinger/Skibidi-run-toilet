using UnityEngine;
using TMPro; // Wichtig für TextMesh Pro!

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText; 

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
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skibidi: " + score;
        }
    }
}
