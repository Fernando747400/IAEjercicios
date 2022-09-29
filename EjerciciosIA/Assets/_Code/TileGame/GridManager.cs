using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Settings")]
    [SerializeField] private float _floodDelay;

    private List<GameObject> seeds = new List<GameObject>();
    private GameObject[,] _grid;

    public GameObject[,] Grid { get => _grid; set { _grid = value; } }

    private void Awake()
    {
        Instance = this;
    }

    public void SubscribeToSeedEvents()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach (var tile in tiles)
        {
            tile.SeedEvent += NewSeed;
        }
    }

    private void StartFlood()
    {
        
    }

    private void NewSeed(GameObject seed)
    {
        if (!seeds.Contains(seed)) seeds.Add(seed);
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
    }
}
