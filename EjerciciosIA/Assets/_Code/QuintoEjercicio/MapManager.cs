using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _prefab;
    private Vector2Int _size;
    private bool _isIso;
    private float _offset;
    private Color _selectBlock;
    private Color _startColor;
    private Color _endColor;
    private bool _goalSelected;
    private Fields _fields;
    private Map _map;

    #region UnityMethods
    private void Start()
    {
        _map = GetComponent<Map>();
        _map.Height = 5;
        _map.Width = 5;
        _map.Offset = 0.5f;
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
