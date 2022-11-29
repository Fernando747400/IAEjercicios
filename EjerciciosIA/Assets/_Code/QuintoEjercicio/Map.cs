using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private GameObject[,] _map;
    private int _height;
    private int _width;
    private Vector2 _rotX;
    private Vector2 _rotY;
    private Vector2[] _isoPoints;
    private float _xOffset;
    private float _yOffset;
    private int _order;
    private bool _isIso;
    private Block _start;
    private Block _goal;

    private List<Block> _starsList = new List<Block>();
    private List<Block> _goalsList = new List<Block>();

    public event Action FinishedMapCreationEvent;

    public GameObject[,] MapCurrent { get => _map;}
    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }
    public float XOffset { get => _xOffset; set => _xOffset = value; }
    public float YOffset { get => _yOffset; set => _yOffset = value; }
    public Vector2[] IsoPoints { get => _isoPoints; set => _isoPoints = value; }
    public bool IsIso { get => _isIso; set => _isIso = value; }
    public Block Start { get => _start; set => _start = value; }
    public Block Goal { get => _goal; set => _goal = value; }

    public List<Block> Seeds { get => _starsList;}
    public List<Block> Goals { get => _goalsList;}

    public GameObject[,] CreateMap(GameObject tile, Sprite sprite = null, bool isIso = false)
    {
        _order = _width * _height;
        _map = new GameObject[_width,_height];

        float currentYOff = 0;
        for (int i = 0; i < _height; i++)
        {
            float currentXOff = 0;
            for (int j = 0; j < _width; j++)
            {
                GameObject tileBlock = Instantiate(tile, this.transform);
                SpriteRenderer renderer = tileBlock.GetComponent<SpriteRenderer>();
                tileBlock.name = $"{i}-{j}";

                AddMissingComponents(tileBlock);

                Block block = tileBlock.GetComponent<Block>();
                block.Coordinates = new Vector2Int(j,i);
                block.BlockStateType = Block.BlockState.FREE;
                block.SeedEvent += AddSeed;
                block.GoalEvent += AddGoal;
                block.ClearEvent += RemoveFromList;

                float size = block.transform.lossyScale.x;
                block.transform.position = new Vector3((size + _xOffset) * (0.5f + j), (size * _yOffset) * (0.5f + i), 0);

                if (_isIso) CreateIsoMap(tileBlock, renderer, j, i);
                _map[j, i] = tileBlock;
                currentXOff += _xOffset;
            }
            currentYOff += _yOffset;
        }
        CenterMap();
        FinishedMapCreationEvent?.Invoke();
        return _map; //TODO ISO map creation
    }

    public void CreateIsoMap(GameObject prefab, SpriteRenderer renderer, int x, int y)
    {
        Destroy(prefab.GetComponent<BoxCollider2D>());
        prefab.AddComponent<PolygonCollider2D>();
        PolygonCollider2D polygonCollider = prefab.GetComponent<PolygonCollider2D>();
        polygonCollider.points = _isoPoints;

        _rotX = new Vector2(0.5f * (renderer.bounds.size.x + _xOffset), 0.25f * (renderer.bounds.size.y + _yOffset));
        _rotY = new Vector2(-0.5f * (renderer.bounds.size.x + _xOffset), 0.25f * (renderer.bounds.size.y + _yOffset));
        Vector2 rotate = (x * _rotX) + (y * _rotY);
        prefab.transform.position = rotate;
        renderer.sortingOrder = _order;
        _order--;
    }

    public void AddMissingComponents(GameObject block)
    {
        if (block.GetComponent<Block>() == null) block.AddComponent<Block>();
        if (block.GetComponent<BoxCollider2D>() == null)
        {
            block.AddComponent<BoxCollider2D>();
            BoxCollider2D boxCollider = block.GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
        }
    }

    public void CenterMap()
    {
        float offsetX = (_map[_width-1, 0].transform.position.x + _map[0,_height -1].transform.position.x) /2f;
        float offsetY = ((_map[_width - 1, _height - 1].transform.position.y)/2f + _map[_width-1, _height - 1].transform.lossyScale.x/4);
        transform.position += new Vector3(-offsetX, -offsetY,0);
    }

    private void AddSeed(GameObject seed)
    {
        Block seedBlock = seed.GetComponent<Block>();
        if (seedBlock == null) throw new NullReferenceException("The component" + seed.name + " doesn't have Block scipt for Seed");
        _starsList.Add(seedBlock);
        _start = _starsList[0];
    }

    private void AddGoal(GameObject goal)
    {
        Block goalBlock = goal.GetComponent<Block>();
        if (goalBlock == null) throw new NullReferenceException("The component" + goal.name + " doesn't have Block scipt fro BLock");
        _goalsList.Add(goalBlock);
        _goal = _goalsList[0];
    }

    private void RemoveFromList(GameObject Block)
    {
       Block item = Block.GetComponent<Block>();

       if(_starsList.Contains(item)) _starsList.Remove(item);
       if(_goalsList.Contains(item)) _goalsList.Remove(item);

       if (_starsList.Count != 0) _start = _starsList[0];
       else _start = null;

       if (_goalsList.Count != 0) _goal = _goalsList[0];
       else _goal = null;
    }

    //XOff 7.62   YOFF 9.95
    //ISO XOFF -1.63  YOFF -0.30
}
