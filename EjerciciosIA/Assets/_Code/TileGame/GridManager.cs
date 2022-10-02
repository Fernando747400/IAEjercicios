using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Settings")]
    [SerializeField] private float _floodDelay;

    private List<GameObject> _seeds = new List<GameObject>();
    private GameObject[,] _grid;

    public GameObject[,] Grid { get => _grid; set { _grid = value; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFlood();
        }
    }

    public void SubscribeToSeedEvents()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach (var tile in tiles)
        {
            tile.SeedEvent += NewSeed;
        }
    }

    public void StartFlood()
    {
        foreach (var seed in _seeds)
        {
            Vector2 seedCoordinates = GetSeedCoordinates(seed);
            StartCoroutine(Flood((int)seedCoordinates.x, (int)seedCoordinates.y));
        }
    }

    private void NewSeed(GameObject seed)
    {
        if (!_seeds.Contains(seed)) _seeds.Add(seed);
    }

    private Vector2 GetSeedCoordinates(GameObject seed)
    {
        Vector2 coordinates;
        string[] coordinate = seed.name.Split('-');
        coordinates.x = int.Parse(coordinate[0]);
        coordinates.y = int.Parse(coordinate[1]);
        return coordinates;
    }

    IEnumerator Flood(int x, int y)
    {
      yield return new WaitForSeconds(_floodDelay);

        if (x >= 0 && x < _grid.GetLength(0) && y >= 0 && y < _grid.GetLength(1))
        {
            Tile currentTile = _grid[x, y].GetComponent<Tile>();
            if (currentTile.State == Tile.TileState.EMPTY || currentTile.State == Tile.TileState.SEED)
            {
                currentTile.State = Tile.TileState.FLOODED;
                currentTile.ChangeColorByState();
                StartCoroutine(Flood(x + 1, y));
                StartCoroutine(Flood(x - 1, y));
                StartCoroutine(Flood(x, y + 1));
                StartCoroutine(Flood(x, y - 1));
            }
        }
    }
}
