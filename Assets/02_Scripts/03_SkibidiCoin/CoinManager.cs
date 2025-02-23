using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Color _collectionFlashColor;
    private Color _baseColor;
    
    public static CoinManager instance;
    public float score = 0.42069f;
    public TextMeshProUGUI scoreText;

    // Event für Änderungen am Score
    public event Action OnScoreChanged;

    private void Update()
    {
        if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.S)
            && Input.GetKey(KeyCode.D))
        {
            AddScore(1000);
        }
    }

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

        _baseColor = scoreText.color;
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
            scoreText.text = score.ToString();

            scoreText.color = _collectionFlashColor;
            scoreText.transform.DOScale(Vector3.one * 2f, 0.2f).OnComplete(() =>
            {
                scoreText.transform.DOScale(Vector3.one * 0.5f, 0.1f).OnComplete(() =>
                {
                    scoreText.transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>
                    {
                        scoreText.color = _baseColor;
                    });
                });
            });
        }
    }
}
