using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShyBrackey : MonoBehaviour
{
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;
    public float visibleTime = 2f;
    public int coinValue = 1;
    public float initialVisibleDuration = 5f;
    private Tween shakeTween;
    private Button button;
    public RectTransform buttonRect;
    public Canvas canvas;  // Verweise auf den Canvas anstatt RectTransform
    private RectTransform canvasRect;  // Referenz für das RectTransform
    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        canvasRect = canvas.GetComponent<RectTransform>();  // Hole das RectTransform vom Canvas
        button = buttonRect.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        buttonRect.gameObject.SetActive(true);
        StartShakingEffect();
        StartCoroutine(InitialVisibility());

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    
    private IEnumerator InitialVisibility()
    {
        yield return new WaitForSeconds(initialVisibleDuration);
        buttonRect.gameObject.SetActive(false);
        StopShakingEffect();
        StartCoroutine(SpawnButton());
    }
    
    private IEnumerator SpawnButton()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            MoveButtonToRandomPosition();
            buttonRect.gameObject.SetActive(true);
            StartShakingEffect();
            yield return new WaitForSeconds(visibleTime);
            StopShakingEffect();
            buttonRect.gameObject.SetActive(false);
        }
    }
    
    private void MoveButtonToRandomPosition()
    {
        Vector2 canvasSize = canvasRect.rect.size;
        float x = Random.Range(buttonRect.rect.width / 2, canvasSize.x - buttonRect.rect.width / 2);
        float y = Random.Range(buttonRect.rect.height / 2, canvasSize.y - buttonRect.rect.height / 2);
        buttonRect.anchoredPosition = new Vector2(x - canvasSize.x / 2, y - canvasSize.y / 2);
    }
    
    public void OnButtonClick()
    {
        CoinManager.instance.AddScore(coinValue);
        audioSource.PlayOneShot(clickSound, 0.3f);
        StopShakingEffect();
        buttonRect.gameObject.SetActive(false);
    }
    
    private void StartShakingEffect()
    {
        shakeTween = buttonRect.DOShakePosition(0.5f, 10f, 10, 90, false, true)
            .SetLoops(-1, LoopType.Yoyo);
        buttonRect.DOScale(1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void StopShakingEffect()
    {
        shakeTween?.Kill();
        buttonRect.DOKill();
        buttonRect.localScale = Vector3.one;
    }
}
