using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 5f; // Höhe des Sprungs
    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if (JumpAllowed())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private bool JumpAllowed()
    {
        return (isGrounded || JumpBufferingCheck()) && !IsGoingUp();
    }
    
    private bool IsGoingUp()
    {
        return rb.linearVelocity.y > 0;
    }
    
    private bool IsGoingDown()
    {
        return rb.linearVelocity.y < 0;
    }

    private bool JumpBufferingCheck()
    {
        Vector2 overLapPos = transform.position + new Vector3(0, -0.25f, 0);
        var result = Physics2D.OverlapCircle(overLapPos, 1f);
        return result.CompareTag("Ground");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                CameraShaker.Instance.DefaultShake();
                Vector3 particlePos = new Vector3(transform.position.x, collision.contacts[0].point.y,
                    transform.position.z);
                ParticleProvider.Instance.SpawnHitParticles(particlePos, collision.contacts[0].normal);
            }
            isGrounded = true;
            // CAMERA SHAKE ON GROUND HIT
           
            
        }
    }
}
