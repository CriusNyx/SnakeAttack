using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridtutorial : MonoBehaviour
{
    public static IEnumerable<GridNode> CreateAGrid()
    {
        //
        int width = 100, height = 100;
        GridNode[,] gridNodes = new GridNode[width, height];
        List<GridNode> output = new List<GridNode>();
        //
        for(int x=0; x<width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //create new grid node
                //create takes two params, position and name
                GridNode node = GridNode.Create(new Vector3(x, y, 0), "GridNode:(" + x + "," + y + ")");
                //put this gridnode into the array
                gridNodes[x, y] = node;
                //put the gridnode into the output list
                output.Add(node);
            }
        }
        //link the gridnodes
        GridNode.AutoLink(gridNodes);
        //return the list
        return output;

    }






}
