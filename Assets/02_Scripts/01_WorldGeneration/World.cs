using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class World : MonoBehaviour
{
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private List<GameObject> _randomSegmentPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _defaultSegmentPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _orderSegmentPrefabs = new List<GameObject>();
    private Dictionary<int, GameObject> _orderedSegmentDict = new Dictionary<int, GameObject>();

    private List<GameObject> _spawnedSegments = new List<GameObject>();
    private float _currentLength;
    
    private int _currentSegmentCount = 1;
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GenerateNextSegment();
        }
    }

    private void Init()
    {
        foreach (var segment in _orderSegmentPrefabs)
        {
            var segmentScript = segment.GetComponent<Segment>();
            _orderedSegmentDict.Add(segmentScript.GetPosition(), segment);
            _currentLength += _startPos.x;
        }
    }
    
    private bool IsSegmentOrdered(int position)
    {
        return _orderedSegmentDict.ContainsKey(position);
    }
    
    private GameObject GetNextSegment()
    {
        if (IsSegmentOrdered(_currentSegmentCount))
        {
            return _orderedSegmentDict[_currentSegmentCount];
        }
        return _randomSegmentPrefabs.Count == 0 ? 
            _defaultSegmentPrefabs[Random.Range(0, _defaultSegmentPrefabs.Count)] 
            : _randomSegmentPrefabs[Random.Range(0, _randomSegmentPrefabs.Count)];
    }
    
    public void GenerateNextSegment()
    {
        var segment = GetNextSegment();
        var segmentScript = segment.GetComponent<Segment>();
        var pos = _startPos + new Vector3(_currentLength, 0, 0);
        var temp = Instantiate(segment, pos, Quaternion.identity);
        
        AddSpawnedSegment(temp);
        _currentSegmentCount++;
    }

    private void AddSpawnedSegment(GameObject spawnedSegment)
    {
        Segment seg = spawnedSegment.GetComponent<Segment>();
        _spawnedSegments.Add(spawnedSegment);
        _currentLength += seg.GetLength();
    }
    
}
