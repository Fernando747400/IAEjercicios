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
                GameObject block = Instantiate(tile);
                block.transform.parent = transform;
                block.name = $"{i}-{j}";
                currentMap[j, i] = block;
                currentXOff += _offset;
                if (sprite == null) continue;
                block.GetComponent<SpriteRenderer>().sprite = sprite;
            }
            currentYOff += _offset;
        }
        return currentMap; //TODO ISO map creation
    }

    public void AddComponents(GameObject component)
    {
        if (component.GetComponent<Block>() == null) component.AddComponent<Block>();
        
    }

    public void CreateIsoMap(GameObject prefab, SpriteRenderer renderer, int x, int y)
    {
        _rotX = new Vector2(0.5f * (renderer.bounds.size.x + _offset), 0.25f * (renderer.bounds.size.y + _offset));
        _rotY = new Vector2(-0.5f * (renderer.bounds.size.x + _offset), 0.25f * (renderer.bounds.size.y + _offset));
        Vector2 rotate = (x * _rotX) + (y * _rotY);
        prefab.transform.Rotate(rotate);
    }

    public void CenterMap()
    {

    }
}
