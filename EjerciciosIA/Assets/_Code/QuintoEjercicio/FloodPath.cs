using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodPath : MonoBehaviour
{
    private Queue<Block> _frontier;
    private Dictionary<Block, Block> _cameFrom;
    private Map _map;
    private MapManager _manager;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void GetPath()
    {

    }

    private void GetNeighbours (Block home)
    {
        int x = home.Coordinates.x;
        int y = home.Coordinates.y;

        if (CheckLimits(x + 1, y)) AddNext(home, x + 1, y);
    }

    private bool CheckLimits (int x, int y)
    {
        return false; //TODO
    }

    private void AddNext(Block home, int x, int y)
    {

    }

    private void PrintPath()
    {

    }
}
