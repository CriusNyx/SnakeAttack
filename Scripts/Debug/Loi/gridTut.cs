using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gridTut : MonoBehaviour {


    private GridTransform gridTransform;
    private List<GridNode> grid;
    //Vector3 dir = Vector3.right;
    bool ate = false;
    public GameObject tailPrefab;
    List<Transform> tail = new List<Transform>();
    Vector2 dir = Vector2.right;

    private void Start () {
        //InvokeRepeating("Move", 0.3f,0.3f);
        //grid = MoveTut.CreateAGrid().ToList();
        gridTransform = gameObject.AddComponent<GridTransform>();

        gridTransform.Warp(GridSystem.GetNode(0,0));
        

    }
    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                  v,
                                                  Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("foodprefab"))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
            // ToDo 'You lose' screen
        }
    }

    // Update is called once per frame
    private void Update () {
    
        GridNode currentNode = gridTransform.CurrentNode;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //move right
            gridTransform.MoveTo(currentNode.right);
            //dir = Vector3.right;

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            //move left
            gridTransform.MoveTo(currentNode.left);
            //dir = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //move up
            gridTransform.MoveTo(currentNode.top);
            //dir = Vector3.up;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //move down
            gridTransform.MoveTo(currentNode.bottom);
            //dir = Vector3.down;
        }
        //move the 3d position to grid positions
        transform.position = gridTransform.Target;
        
    }
}



