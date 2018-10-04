using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    public static IEnumerable<GridNode> CreateAGrid()
    {
        //Initialize the array for linking, and the list for output
        int width = 100, height = 100;
        GridNode[,] gridNodes = new GridNode[width, height];
        List<GridNode> output = new List<GridNode>();

        //loop through each node
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //Create a new grid node
                //Create takes two params, position and name
                GridNode node = GridNode.Create(new Vector3(x, y, 0), "GridNode:(" + x + "," + y + ")");
                //Put the grid node in the array
                gridNodes[x, y] = node;
                //Put the grid node in the output list
                output.Add(node);
            }
        }

        //Link the nodes
        GridNode.AutoLink(gridNodes);

        //return the list
        return output;
    }
}