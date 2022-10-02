using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public TileState State;

    public event Action<GameObject> SeedEvent;

    private SpriteRenderer _spriteRenderer;
    private Color _initialColor;
   
    public enum TileState
    {
        EMPTY,
        SEED,
        OBSTACLE,
        FLOODED
    }

    private void Start()
    {
        Prepare();
    }

    private void OnMouseOver()
    {
        _spriteRenderer.color = Color.cyan;
        if (Input.GetMouseButtonDown(0)) ChangeStateOnInput(KeyCode.Mouse0);
        else if (Input.GetMouseButtonDown(1)) ChangeStateOnInput(KeyCode.Mouse1);
    }

    private void OnMouseExit()
    {
        ChangeColorByState();
    }

    private void ChangeStateOnInput(KeyCode keyPressed)
    {
        switch (keyPressed)
        {
            case KeyCode.Mouse0:
                State = TileState.SEED;
                SeedEvent?.Invoke(this.gameObject);
                break;
            case KeyCode.Mouse1:
                State = TileState.OBSTACLE;
                break;
        }
        ChangeColorByState();
    }

    public void ChangeColorByState()
    {
        switch (State)
        {
            case TileState.EMPTY:
                _spriteRenderer.color = _initialColor;
                break;
            case TileState.SEED:
                _spriteRenderer.color = Color.green;
                break;
            case TileState.OBSTACLE:
                _spriteRenderer.color = Color.red;
                break;
            case TileState.FLOODED:
                _spriteRenderer.color = Color.blue;
                break;
        }
    }

    private void Prepare()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialColor = _spriteRenderer.color;
    }
}
