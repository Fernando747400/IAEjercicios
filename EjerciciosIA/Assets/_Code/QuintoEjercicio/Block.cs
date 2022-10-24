using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockState BlockStateType;
    public event Action<GameObject> SeedEvent;
    public event Action<GameObject> GoalEvent;

    private Vector2Int _coordinates;
    private SpriteRenderer _spriteRenderer;
    private int _moveCost;

    private Color _initialColor;

    public enum BlockState
    {
        FREE,
        SEED,
        GOAL,
        OBSTACLE,
        PATH
    }

    public Vector2Int Coordinates { get => _coordinates; set => _coordinates = value; }
    
    #region UnityMethods
    private void Awake()
    {
        Prepare();
    }

    public void OnMouseOver()
    {
        _spriteRenderer.color = MapManager.Instance.MouseOverColor;
        if (Input.GetMouseButtonDown(0)) ChangeStateOnInput(KeyCode.Mouse0);
        else if (Input.GetMouseButtonDown(1)) ChangeStateOnInput(KeyCode.Mouse1);
        else if (Input.GetMouseButtonDown(2)) ChangeStateOnInput(KeyCode.Mouse2);
    }

    public void OnMouseExit()
    {
        ChangeColorByState();
    }
    #endregion

    #region Private Methods
    private void ChangeStateOnInput(KeyCode keyPressed)
    {
        switch (keyPressed)
        {
            case KeyCode.Mouse0:
                BlockStateType = BlockState.SEED;
                SeedEvent?.Invoke(this.gameObject);
                break;
            case KeyCode.Mouse1:
                BlockStateType = BlockState.OBSTACLE;
                break;
            case KeyCode.Mouse2:
                BlockStateType = BlockState.GOAL;
                GoalEvent?.Invoke(this.gameObject);
                break;
        }
        ChangeColorByState();
    }

    public void ChangeColorByState()
    {
        switch (BlockStateType)
        {
            case BlockState.FREE:
                _spriteRenderer.color = _initialColor;
                break;
            case BlockState.SEED:
                _spriteRenderer.color = MapManager.Instance.SeedColor;
                break;
            case BlockState.GOAL:
                _spriteRenderer.color = MapManager.Instance.GoalColor;
                break;
            case BlockState.OBSTACLE:
                _spriteRenderer.color = MapManager.Instance.ObstacleColor;
                break;
            case BlockState.PATH:
                _spriteRenderer.color = MapManager.Instance.PathColor;
                break;
        }
    }

    private void Prepare()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialColor = _spriteRenderer.color;
    }
    #endregion
}
