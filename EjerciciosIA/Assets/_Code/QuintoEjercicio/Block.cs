using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockType ObstacleType;

    private Vector2 _coordinates;
    private SpriteRenderer _renderer;
    private Color _color;
    private MapManager _manager;
    private int _moveCost;

    public enum BlockType
    {
        Free,
        Obstacle,
        Water,
        Goal
    }

    public Vector2 Coordinates { get => _coordinates; set => _coordinates = value; }
    

    #region UnityMethods
    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }

    public void OnMouseOver()
    {
        
    }

    public void OnMouseExit()
    {
        
    }
    #endregion
}
