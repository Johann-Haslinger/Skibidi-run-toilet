using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class World : MonoBehaviour
{
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private float _startSpeed;
    [SerializeField] private List<GameObject> _randomSegmentPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _defaultSegmentPrefabs = new List<GameObject>();
    [SerializeField] private bool _forceDefault;
    [SerializeField] private List<GameObject> _orderSegmentPrefabs = new List<GameObject>();
    [SerializeField] private GameObject _startSegment;
   
    
    private Dictionary<int, GameObject> _orderedSegmentDict = new Dictionary<int, GameObject>();

    private List<GameObject> _spawnedSegments = new List<GameObject>();
    private float _currentLength = 0;
    
    private int _currentSegmentCount = 1;

    public static World Instance;

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

    public void AddPossibleSegments(List<GameObject> segments)
    {
        _randomSegmentPrefabs.AddRange(segments);
    }

    private void OnDestroy()
    {
        Instance = null;
    }


    private void Start()
    {
        Init();
        VALUES.WorldSpeed = _startSpeed;
        for (int i = 0; i < 3; i++ )
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
            
        }

        if (_startSegment)
        {
            AddSpawnedSegment(_startSegment);
            _startSegment.transform.position = _startPos;
        }
    }

    private void MoveWorld()
    {
        
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

        if (_forceDefault)
        {
            return _defaultSegmentPrefabs[Random.Range(0, _defaultSegmentPrefabs.Count)];
        }
        
        return _randomSegmentPrefabs.Count == 0 ? 
            _defaultSegmentPrefabs[Random.Range(0, _defaultSegmentPrefabs.Count)] 
            : _randomSegmentPrefabs[Random.Range(0, _randomSegmentPrefabs.Count)];
    }
    
    public void GenerateNextSegment()
    {
        var segment = GetNextSegment();
        var segmentScript = segment.GetComponent<Segment>();
        
        var pos = _spawnedSegments[0].transform.position + new Vector3(_currentLength + segmentScript.GetLength(), 0, 0);
        var temp = Instantiate(segment, pos, Quaternion.identity);
        
        AddSpawnedSegment(temp);
    }

    private void AddSpawnedSegment(GameObject spawnedSegment)
    {
        Segment seg = spawnedSegment.GetComponent<Segment>();
        _spawnedSegments.Add(spawnedSegment);
        if (_currentSegmentCount > 1)
        {
            _currentLength += seg.GetLength();
        }
        _currentSegmentCount++;
        
        if (_spawnedSegments.Count > 10)
        {
            var temp = _spawnedSegments[0];
            _currentLength -= temp.GetComponent<Segment>().GetLength();
            
            _spawnedSegments.RemoveAt(0);
            Destroy(temp);
        }
    }
    
}
