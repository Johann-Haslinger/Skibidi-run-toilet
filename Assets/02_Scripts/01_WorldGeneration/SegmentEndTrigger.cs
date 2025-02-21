using UnityEngine;

public class SegmentEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            World.Instance.GenerateNextSegment();
            Destroy(gameObject);
        }
    }
}
