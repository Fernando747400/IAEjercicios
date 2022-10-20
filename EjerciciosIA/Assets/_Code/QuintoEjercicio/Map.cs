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
    private Vector2 _isoPoints;
    private float _xOffset;
    private float _yOffset;
    private int _order;
    private bool _isIso;
    private Block _start;
    private Block _goal;

    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }
    public float XOffset { get => _xOffset; set => _xOffset = value; }
    public float YOffset { get => _yOffset; set => _yOffset = value; }
    public Vector2 IsoPoints { get => _isoPoints; set => _isoPoints = value; }
    public bool IsIso { get => _isIso; set => _isIso = value; }

    public GameObject[,] CreateMap(GameObject tile, Sprite sprite = null, bool isIso = false)
    {
        _order = _width * _height;
        GameObject[,] currentMap = new GameObject[_width,_height];

        float currentYOff = 0;
        for (int i = 0; i < _height; i++)
        {
            float currentXOff = 0;
            for (int j = 0; j < _width; j++)
            {
                GameObject tileBlock = Instantiate(tile, this.transform);
                SpriteRenderer renderer = tileBlock.GetComponent<SpriteRenderer>();
                tileBlock.name = $"{i}-{j}";

                AddComponents(tileBlock);

                Block block = tileBlock.GetComponent<Block>();
                block.Coordinates = new Vector2Int(j,i);
                block.ObstacleType = Block.BlockType.Free;

                float size = block.transform.lossyScale.x;
                block.transform.position = new Vector3((size + _xOffset) * (0.5f + j), (size * _yOffset) * (0.5f + i), 0);

                if (_isIso) CreateIsoMap(tileBlock, renderer, j, i);
                currentMap[j, i] = tileBlock;
                currentXOff += _xOffset;
            }
            currentYOff += _yOffset;
        }
        return currentMap; //TODO ISO map creation
    }

    public void AddComponents(GameObject component)
    {
        if (component.GetComponent<Block>() == null) component.AddComponent<Block>();
        
    }

    public void CreateIsoMap(GameObject prefab, SpriteRenderer renderer, int x, int y)
    {
        _rotX = new Vector2(0.5f * (renderer.bounds.size.x + _xOffset), 0.25f * (renderer.bounds.size.y + _yOffset));
        _rotY = new Vector2(-0.5f * (renderer.bounds.size.x + _xOffset), 0.25f * (renderer.bounds.size.y + _yOffset));
        Vector2 rotate = (x * _rotX) + (y * _rotY);
        prefab.transform.position = rotate;
        renderer.sortingOrder = _order;
        _order--;
    }

    public void CenterMap()
    {

    }

    //XOff 7.62   YOFF 9.95
    //ISO XOFF -1.63  YOFF -0.30
}
