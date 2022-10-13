using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodPath : MonoBehaviour
{
    private Queue<Block> _frontier;
    private Dictionary<Block, Block> _comeFrom;
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
