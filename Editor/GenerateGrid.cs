using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class GenerateGrid
{
    [MenuItem("GameObject/2D Object/Snake Grid")]
    public static void CreateGrid()
    {
        GameObject grid = new GameObject("Grid");
        grid.AddComponent<Grid>();

        CreateTilemap(grid, "Tilemap:1", 0);
        CreateTilemap(grid, "Tilemap:2", 1);
        CreateTilemap(grid, "Tilemap:3", 3);
        var collisionMap = CreateTilemap(grid, "Collision", 4);
        collisionMap.AddComponent<CollGenerator>();
    }

    private static GameObject CreateTilemap(GameObject grid, string name, int sortingLayer)
    {
        GameObject tilemap = new GameObject(name);
        tilemap.transform.SetParent(grid.transform);
        tilemap.AddComponent<Tilemap>();
        var renderer = tilemap.AddComponent<TilemapRenderer>();
        renderer.sortingOrder = sortingLayer;

        return tilemap;
    }
}