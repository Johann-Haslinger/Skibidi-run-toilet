using DG.Tweening;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField, Range(0,1)] private float _stretchPercentage = 0.1f;
    private Rigidbody2D _rb;
    private bool _isGrounded = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if (JumpAllowed())
        {
            Jump();
        }
    }

    private bool JumpAllowed()
    {
        return (_isGrounded || JumpBufferingCheck()) && !IsGoingUp();
    }
    private bool JumpBufferingCheck()
    {
        Vector2 overLapPos = transform.position + new Vector3(0, -0.25f, 0);
        var result = Physics2D.OverlapCircle(overLapPos, 1f);
        return result.CompareTag("Ground");
    }

    private bool IsGoingUp()
    {
        return _rb.linearVelocity.y > 0;
    }
    
    private bool IsGoingDown()
    {
        return _rb.linearVelocity.y < 0;
    }
    
    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
        _isGrounded = false;
        
        JumpingVFX();
    }

    private void JumpingVFX()
    {
        transform.DOScale(new Vector3(1 - _stretchPercentage, 1 + _stretchPercentage, 1), 0.1f);
        Vector3 particlePos = new Vector3(transform.position.x, transform.position.y - 0.5f,
            transform.position.z);
        ParticleProvider.Instance.SpawnHitParticles(particlePos, transform.up);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bool comingFromAbove = collision.transform.position.y < transform.position.y;
            if (!_isGrounded && comingFromAbove)
            {
                
                Land(collision);
            }
        }
    }

    private void Land(Collision2D collision)
    {
        _isGrounded = true;
        LandingVFX(collision);
    }

    private void LandingVFX(Collision2D collision)
    {
        transform.DOScale(new Vector3(1 + (_stretchPercentage * 2) ,1 - _stretchPercentage, 1), 0.1f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.1f);
        });
        CameraShaker.Instance.DefaultShake();
    }
}
