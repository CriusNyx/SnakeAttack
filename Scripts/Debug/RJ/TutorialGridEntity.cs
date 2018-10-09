using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialGridEntity : MonoBehaviour
{
    private GridTransform gridTransform;
    private List<GridNode> grid;

    private void Start()
    {
        //Create a grid using the method created in part 1
        //This will be available on github
        grid = MovementTutorial.CreateAGrid().ToList();

        //Add a grid transform to the object for grid movement
        gridTransform = gameObject.AddComponent<GridTransform>();

        //Warp the grid transform to the first space on the grid (lower left corner)
        gridTransform.Warp(grid[0]);
    }

    private void Update()
    {
        //Get the node this object is currently on
        GridNode currentNode = gridTransform.CurrentNode;

        //Get the input, and move the object if an input is pressed
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //move right
            gridTransform.MoveTo(currentNode.right);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //move left
            gridTransform.MoveTo(currentNode.left);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //move up
            gridTransform.MoveTo(currentNode.top);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //move down
            gridTransform.MoveTo(currentNode.bottom);
        }

        //Move the 3d position to the grid positions
        transform.position = gridTransform.Target;
    }
}