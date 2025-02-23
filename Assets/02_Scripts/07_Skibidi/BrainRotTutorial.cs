using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrainRotTutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text _tutorialText;
    [SerializeField] private Vector2 _fontSizeRange;
    [SerializeField] private float _changeInterval;
    [SerializeField] private float _lifeTime;
    [SerializeField] private List<Color> _colors;

    private float _timeSinceLastChange;
    
    private void Update()
    {
        _timeSinceLastChange += Time.deltaTime;
        if (_timeSinceLastChange > _changeInterval)
        {
            _tutorialText.fontSize = Random.Range(_fontSizeRange.x, _fontSizeRange.y);
            if (_tutorialText.isUsingBold)
            {
                _tutorialText.fontStyle = FontStyles.Italic;
            }
            else
            {
                _tutorialText.fontStyle = FontStyles.Bold;
            }
            _timeSinceLastChange = 0;
            _tutorialText.color = _colors[Random.Range(0, _colors.Count)];
        }

        if (Time.time > _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
