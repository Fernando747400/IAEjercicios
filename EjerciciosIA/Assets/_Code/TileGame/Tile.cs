using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent (typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] public TileState State;

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
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = _initialColor;
    }

    private void OnMouseUpAsButton()
    {
        //ChangeColor(Input.GetKeyDown());
    }

    private void ChangeColor(KeyCode mouseKey)
    {

    }

    private void Prepare()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialColor = _spriteRenderer.color;
    }
}
