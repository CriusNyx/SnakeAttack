using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton class make grids for debugging
/// </summary>
public class GridSystem : MonoBehaviourSingleton
{
    static GridSystem Instance
    {
        get
        {
            return GetInstance<GridSystem>();
        }
    }

    const int width = 100;
    private GameObject parent;
    GridNode[,] gridNodes;

    int xOffset = 0;
    int yOffset = 0;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        parent = new GameObject("Grid");

        gridNodes = CollGenerator.GetGrid(out xOffset, out yOffset);
        if(gridNodes != null)
        {
            int width = gridNodes.GetLength(0);
            int height = gridNodes.GetLength(1);
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    if(gridNodes[x, y] != null)
                        gridNodes[x, y].transform.parent = parent.transform;
                }
            }
        }
        else
        {
            gridNodes = new GridNode[width, width];

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < width; y++)
                {
                    GridNode node = new GameObject("GridNode(" + x + "," + y + ")").AddComponent<GridNode>();
                    node.transform.parent = parent.transform;
                    node.transform.position = new Vector3(x, y);
                    gridNodes[x, y] = node;
                }
            }
            GridNode.AutoLink(gridNodes);
        }

    }

    public static GridNode GetNode(int x, int y)
    {
        return Instance.GetNodePrivate(x, y);
    }

    private GridNode GetNodePrivate(int x, int y)
    {
        x += xOffset;
        y += yOffset;
        try
        {
            return gridNodes[x, y];
        }
        catch
        {
            return null;
        }
    }
}