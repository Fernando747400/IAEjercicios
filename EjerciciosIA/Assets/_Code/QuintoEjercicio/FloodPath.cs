using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class FloodPath : MonoBehaviour
{
    private Queue<Block> _frontier;
    private Dictionary<Block, Block> _comeFrom;
    private Map _map;
    //private MapManager _manager;

    private Block _seed;
    private Block _goal;

    void Start()
    {
        _map = this.gameObject.GetComponent<Map>();
        _frontier = new Queue<Block>();
        _comeFrom = new Dictionary<Block, Block>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Is Pressed");
            foreach(Block seed in _map.Seeds)
            {
                foreach (Block goal in _map.Goals)
                {
                    _seed = seed;
                    _goal = goal;
                    getPath(seed);
                }
            }
        }
    }

    public void getPath(Block seedBlock)
    {
        _frontier.Enqueue(seedBlock);
        _comeFrom[seedBlock] = null;
        while(_frontier.Count > 0)
        {
            Block current = _frontier.Dequeue();
            GetNeighbours(current);
        }
        printPath();
    }

    private void GetNeighbours(Block current)
    {
        if (checkLimits(current.Coordinates.x + 1, current.Coordinates.y)) addNext(current, current.Coordinates.x + 1, current.Coordinates.y);
        if (checkLimits(current.Coordinates.x - 1, current.Coordinates.y)) addNext(current, current.Coordinates.x - 1, current.Coordinates.y);
        if (checkLimits(current.Coordinates.x, current.Coordinates.y + 1)) addNext(current, current.Coordinates.x, current.Coordinates.y + 1);
        if (checkLimits(current.Coordinates.x, current.Coordinates.y - 1)) addNext(current, current.Coordinates.x, current.Coordinates.y - 1);
    }

    private bool checkLimits(int x, int y)
    {
        if(x < 0 || x >= _map.Width || y < 0 || y >= _map.Height)
        {
            return false;
        }
        return true;
    }

    private void addNext(Block current, int x, int y)
    {
        Block Next = _map.MapCurrent[x, y].GetComponent<Block>();
        if (!_comeFrom.ContainsKey(Next))
        {
            _frontier.Enqueue(Next);
            _comeFrom[Next] = current;
        }
    }

    private void printPath()
    {
        Block previous;
        previous = _comeFrom[_goal];
        while (previous != _seed)
        {
            previous.BlockStateType = Block.BlockState.PATH;
            previous = _comeFrom[previous];  
        }    
        if(previous == _seed)
        {
            previous.BlockStateType = Block.BlockState.SEED;
        }

        ClearValues();
    }

    private void ClearValues()
    {
        _frontier.Clear();
        _comeFrom.Clear();
    }
}
