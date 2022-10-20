using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _prefab;

    [Header("Map Size")]
    [SerializeField]  private Vector2Int _size;

    [Header("Map settings")]
    [SerializeField] private bool _isIso;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;

    [Header("Interaction Colors")]
    [SerializeField] private Color _selectBlock;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    [Header("Goal Settings")]
    [SerializeField] private bool _goalSelected;

    [Header("Map pieces")]
    [SerializeField] private Fields _fields;


    private Map _map;

    #region UnityMethods
    private void Start()
    {
        _map = GetComponent<Map>();
        _map.Height = _size.y;
        _map.Width = _size.x;
        _map.XOffset = _xOffset;
        _map.YOffset = _yOffset;
        _map.IsIso = _isIso;
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
