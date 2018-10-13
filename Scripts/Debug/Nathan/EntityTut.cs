using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTut : MonoBehaviour {

    private GridTransform gridTransform;
    private List<GridNode> grid;
    //Use this for intialization
	void Start () {
        //Add a grid transform here for grid movement
        gridTransform = gameObject.AddComponent<GridTransform>();
        //Warp the grid transform to the first space on the grid (lower left corner)
        gridTransform.Warp(GridSystem.GetNode(0, 0));
	}
	
	// Update is called once per frame
	void Update () {
        //Get the node that the object is currently on
        GridNode currentNode = gridTransform.CurrentNode;
        //Get the input, and move the object if an input is pressed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //move right
            gridTransform.MoveTo(currentNode.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //move left
            gridTransform.MoveTo(currentNode.left);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //move up
            gridTransform.MoveTo(currentNode.top);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //move down
            gridTransform.MoveTo(currentNode.bottom);
        }

        //move the 3d position to the grid position
        transform.position = gridTransform.Target;

	}
}
