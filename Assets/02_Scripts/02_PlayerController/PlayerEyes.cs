using System;
using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
   [SerializeField] private GameObject _eyeMiddlePoint;
   [SerializeField] private GameObject _eyeBall;
   [SerializeField] private float _range;

   private void Update()
   {
      var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      var direction = mousePos - _eyeMiddlePoint.transform.position;
      direction = direction.normalized;
      _eyeBall.transform.position = _eyeMiddlePoint.transform.position + direction * _range;
   }
}
