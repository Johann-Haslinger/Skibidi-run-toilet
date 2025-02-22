using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip collisionSound;
    [SerializeField] private Color _wrongColor;
    [SerializeField] private Color _baseColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            // Sound abspielen
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);

            // Punkte hinzufügen
            CoinManager.instance.AddScore(coinValue); 
            
            ParticleProvider.Instance.SpawnCollectParticles(transform.position);
            
            // Objekt zerstören
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        _baseColor = GetComponent<SpriteRenderer>().color;
        Vector3 startPos = transform.localPosition;
        
        transform.DOLocalMoveY(startPos.y + 0.2f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public void DestroyCoin()
    {
        var sR = GetComponent<SpriteRenderer>();
        sR.color = _wrongColor;
        var baseScale = transform.localScale;
        transform.DOScale(baseScale * 1.2f, 0.1f).OnComplete(() =>
        {
            //sR.color = _baseColor;
            transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}
