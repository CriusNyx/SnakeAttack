using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollGenerator : MonoBehaviour
{
    Tilemap collMap;
    bool initialized = false;
    GridNode[,] grid;
    int xOffset, yOffset;

    public void Awake()
    {
        //PrivateInit();
    }

    /// <summary>
    /// If a coll generator exists in the scene, it generates it's grid, and returns it
    /// Else it returns null
    /// </summary>
    /// <returns></returns>
    public static GridNode[,] GetGrid(out int xOffset, out int yOffset)
    {
        CollGenerator generator = FindObjectOfType<CollGenerator>();
        if(generator == null)
        {
            xOffset = 0;
            yOffset = 0;
            return null;
        }
        else
        {
            var output = generator.PrivateInit();
            xOffset = generator.xOffset;
            yOffset = generator.yOffset;
            return output;
        }
    }

    private GridNode[,] PrivateInit()
    {
        if(initialized)
            return grid;
        initialized = true;

        collMap = gameObject.GetComponent<Tilemap>();
        if(collMap == null)
            throw new System.InvalidOperationException("Attempted to generate collisions for a gameObject without a collision map.\n" +
                "Make sure to create a proper grid using MenuItem GameObject/2D Object/Snake Grid");

        Vector2Int min, max;
        GetBounds(out min, out max);
        GenerateNodes(min, max);

        GetComponent<TilemapRenderer>().enabled = false;

        return grid;
    }

    private void GetBounds(out Vector2Int min, out Vector2Int max)
    {
        min = new Vector2Int(int.MaxValue, int.MaxValue);
        max = new Vector2Int(int.MinValue, int.MinValue);

        foreach(var tilemap in transform.parent.GetComponentsInChildren<Tilemap>())
        {
            for(int i = 0; i < 2; i++)
            {
                min[i] = Mathf.Min(min[i], tilemap.cellBounds.min[i]);
                max[i] = Mathf.Max(max[i], tilemap.cellBounds.max[i]);
            }
        }

        xOffset = -min.x;
        yOffset = -min.y;
    }

    private void GenerateNodes(Vector2Int min, Vector2Int max)
    {
        int width = max.x - min.x + 1;
        int height = max.y - min.y + 1;
        grid = new GridNode[width, height];

        for(int x = min.x; x <= max.x; x++)
        {
            for(int y = min.y; y <= max.y; y++)
            {
                var tile = collMap.GetTile(new Vector3Int(x, y, 0));
                if(tile == null)
                {
                    GridNode node = GridNode.Create(new Vector3(x + 0.5f, y + 0.5f, -0.01f), "GridNode:(" + x + "," + y + ")");
                    grid[x - min.x, y - min.y] = node;
                }
            }
        }

        GridNode.AutoLink(grid);
    }
}