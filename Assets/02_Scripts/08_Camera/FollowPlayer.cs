using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Vector3 _baseOffset;

    private void Start()
    {
        _baseOffset = transform.position - _player.transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(_baseOffset.x + _player.transform.position.x, transform.position.y, transform.position.z);
    }
}

