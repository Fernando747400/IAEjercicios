using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [Header("Dependencies")]
    [SerializeField] private GameObject _prefab;

    [Header("Map Size")]
    [SerializeField]  private Vector2Int _size;

    [Header("Map settings")]
    [SerializeField] private bool _isIso;
    [SerializeField] private Vector2[] _isoPoints;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;

    [Header("Interaction Colors")]
    [SerializeField] private Color _mouseOverColor;
    [SerializeField] private Color _seedColor;
    [SerializeField] private Color _obstacleColor;
    [SerializeField] private Color _pathColor;
    [SerializeField] private Color _goalColor;

    [Header("Goal Settings")]
    [SerializeField] private bool _goalSelected;

    [Header("Map pieces")]
    [SerializeField] private Fields _fields;

    private Map _map;

    public Color MouseOverColor { get => _mouseOverColor; }
    public Color SeedColor { get => _seedColor; }
    public Color ObstacleColor { get => _obstacleColor; }
    public Color PathColor { get => _pathColor; }
    public Color GoalColor { get => _goalColor; }

    #region UnityMethods

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _map = GetComponent<Map>();
        _map.Height = _size.y;
        _map.Width = _size.x;
        _map.XOffset = _xOffset;
        _map.YOffset = _yOffset;
        _map.IsIso = _isIso;
        _map.IsoPoints = _isoPoints;
        _map.CreateMap(_prefab);
    }

    private void Update()
    {
        
    }
    #endregion

    public void RestartMap()
    {

    }

    public void UpdatePoints(Vector2Int coordinates)
    {

    }

    public void UpdateObstacle(Vector2Int coordinates)
    {

    }
}
