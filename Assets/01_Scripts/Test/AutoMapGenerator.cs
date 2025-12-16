using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class AutoMapGenerator : MonoBehaviour
{
    [Header("Map Settings (Side View)")]
    public int width = 64;
    public int height = 32;

    [Header("Tilemap References")]
    public Tilemap tilemap;
    public Tile grassTile;
    public Tile pathTile;
    public Tile obstacleTile;

    [Header("Waypoints for Enemy Pathing")]
    public List<Vector3> waypoints = new List<Vector3>();

    private System.Random rand = new System.Random();

    void Start()
    {
        GenerateMap();
        CenterTilemap();
    }

    public void GenerateMap()
    {
        tilemap.ClearAllTiles();
        waypoints.Clear();

        Vector2Int spawn = new Vector2Int(0, height / 2);
        Vector2Int goal = new Vector2Int(width - 1, height / 2);

        List<Vector2Int> path = GeneratePath(spawn, goal);

        foreach (var pos in path)
        {
            tilemap.SetTile((Vector3Int)pos, pathTile);
            waypoints.Add(tilemap.CellToWorld((Vector3Int)pos) + new Vector3(0.5f, 0.5f, 0));
        }

        GenerateTerrain(path);
    }

    List<Vector2Int> GeneratePath(Vector2Int start, Vector2Int goal)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int current = start;
        path.Add(current);

        int maxTurns = 10;
        int turnsDone = 0;
        int minTurnDistance = 6; 
        int stepsSinceLastTurn = 0;

        while (current.x < goal.x)
        {
            current.x++;
            stepsSinceLastTurn++;
            path.Add(current);

            if (turnsDone < maxTurns && stepsSinceLastTurn >= minTurnDistance)
            {
                if (Random.value < 0.2f)
                {
                    turnsDone++;
                    stepsSinceLastTurn = 0;

                    int dir = Random.value < 0.5f ? 1 : -1;
                    int steps = Random.Range(5, 12);

                    for (int i = 0; i < steps; i++)
                    {
                        current.y += dir;
                        current.y = Mathf.Clamp(current.y, 2, height - 3);
                        path.Add(current);
                    }
                }
            }
        }

        return path;
    }


    void GenerateTerrain(List<Vector2Int> path)
    {
        HashSet<Vector2Int> usedPath = new HashSet<Vector2Int>(path);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                if (usedPath.Contains(pos))
                    continue;

                tilemap.SetTile((Vector3Int)pos, grassTile);

                if (Random.value < 0.06f)
                    tilemap.SetTile((Vector3Int)pos, obstacleTile);
            }
        }
    }

    void CenterTilemap()
    {
        Vector3 center = new Vector3(width / 2f, height / 2f, 0);
        Vector3 screenCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));
        screenCenter.z = 0;

        tilemap.transform.position = screenCenter - center * tilemap.cellSize.x;
    }

    private void OnDrawGizmos()
    {
        if (waypoints.Count < 2) return;
        Gizmos.color = Color.yellow;

        for (int i = 0; i < waypoints.Count - 1; i++)
            Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
    }
}
