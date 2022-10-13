using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    private GameObject[,] _map;
    private int _height;
    private int _width;
    private Vector2 _rotX;
    private Vector2 _rotY;
    private float _offset;
    private bool _isIso;
    private Block _start;
    private Block _goal;

    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }
    public float Offset { get => _offset; set => _offset = value; }
    public bool IsIso { get => _isIso; set => _isIso = value; }

    public GameObject[,] CreateMap(GameObject tile, Sprite sprite = null, bool isIso = false)
    {
        GameObject[,] currentMap = new GameObject[_width,_height];

        float currentYOff = 0;
        for (int i = 0; i < _height; i++)
        {
            float currentXOff = 0;
            for (int j = 0; j < _width; j++)
            {
                GameObject current = Instantiate(tile, new Vector3(transform.position.x + j + currentXOff, transform.position.y + i + currentYOff, 0), Quaternion.identity);
                current.name = $"{i}-{j}";
                currentMap[i, j] = current;
                currentXOff += _offset;
                if (sprite == null) continue;
                current.GetComponent<SpriteRenderer>().sprite = sprite;
            }
            currentYOff += _offset;
        }
        return currentMap; //TODO ISO map creation
    }

    public void AddComponents(GameObject component)
    {

    }

    public void CreateIsoMap(GameObject prefab, SpriteRenderer renderer, int x, int y)
    {

    }

    public void CenterMap()
    {

    }
}
