using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MoveTut : MonoBehaviour {

	public static IEnumerable<GridNode> CreateAGrid()
    {
        //initialize the array for linking, and the list for output
        int width = 100, height = 100;
        GridNode[,] gridNodes = new GridNode[width, height];
        List<GridNode> output = new List<GridNode>();

        //loop through each node
        for(int x =0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //crete a new grid node
                //create takes two params, position and name
                GridNode node = GridNode.Create(new Vector3(x, y, 0), "GridNode:(" + x + "," + y + ")");
                //put the grid node in the array
                gridNodes[x, y] = node;
                //put the grid node in the output list
                output.Add(node);

            }
        }
        //links the nodes
        GridNode.AutoLink(gridNodes);
        //return the list
        return output;

    }


}
