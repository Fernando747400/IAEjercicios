using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _tile;

    [Header("Settings")]
    [SerializeField] private int _xSize;
    [SerializeField] private int _ySize;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;

    private GameObject[,] _grid;

    public GameObject[,] Grid { get => _grid; }

    private void Start()
    {
        Prepare();
        GenerateGrid(_xSize, _ySize, _xOffset, _yOffset);
        GridManager.Instance.SubscribeToSeedEvents();
        GridManager.Instance.Grid = _grid;
    }

    public void GenerateGrid(int width, int height, float xGap = 0, float yGap = 0)
    {      
        float currentYOff = 0;
        for (int i = 0; i < height; i++)
        {
            float currentXOff = 0;
            for (int j=0; j < width; j++)
            {
                GameObject current = Instantiate(_tile, new Vector3(transform.position.x + j + currentXOff, transform.position.y + i + currentYOff, 0), Quaternion.identity);
                current.name = $"{i}-{j}";
                _grid[i , j] = current;
                currentXOff += xGap;
            }          
            currentYOff += yGap;
        }
    }

    private void Prepare()
    {
        _grid = new GameObject[_ySize, _xSize];
    }
}
