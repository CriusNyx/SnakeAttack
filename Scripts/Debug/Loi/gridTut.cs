using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gridTut : MonoBehaviour {


    private GridTransform gridTransform;
    private List<GridNode> grid;
    

    private void Start () {
        //grid = MoveTut.CreateAGrid().ToList();
        gridTransform = gameObject.AddComponent<GridTransform>();

        gridTransform.Warp(GridSystem.GetNode(0,0));

    }
	
	// Update is called once per frame
	private void Update () {
        
        GridNode currentNode = gridTransform.CurrentNode;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            //move right
            gridTransform.MoveTo(currentNode.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            //move left
            gridTransform.MoveTo(currentNode.left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //move up
            gridTransform.MoveTo(currentNode.top);
        }



        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //move down
            gridTransform.MoveTo(currentNode.bottom);
        }
        //move the 3d position to grid positions
        transform.position = gridTransform.Target;
	}
}



