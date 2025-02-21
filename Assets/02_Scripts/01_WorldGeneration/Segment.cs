using UnityEngine;

public class Segment : MonoBehaviour
{
   
   [SerializeField] private float _length;
   [SerializeField] private int _forcePosition = -1;
   
   public int GetPosition()
   {
      return _forcePosition;
   }
   
   public float GetLength()
   {
      return _length;
   }
}
