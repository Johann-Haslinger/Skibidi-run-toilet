using System;
using UnityEngine;

public class ParticleProvider : MonoBehaviour
{
    
    [SerializeField] private GameObject _hitParticles;
    [SerializeField] private GameObject _collectParticles;
    
    [SerializeField] private float  _maxLifeTime;

    public static ParticleProvider Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnHitParticles(Vector3 position, Vector3 direction)
    {
        var temp = Instantiate(_hitParticles, position, Quaternion.identity);
        temp.transform.up = direction;
        Destroy(temp, _maxLifeTime);
    }

    public void SpawnCollectParticles(Vector3 position)
    {
        var temp = Instantiate(_collectParticles, position, Quaternion.identity);
        Destroy(temp, _maxLifeTime);
    }
}
