using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //Nathan Haberland

    private GridTransform gridTransform;
    private List<Player_Tail> tail = new List<Player_Tail>();


    // Use this for initialization
    void Start()
    {
        gridTransform = gameObject.AddComponent<GridTransform>();
        gridTransform.Warp(GridSystem.GetNode(0, 0));


        var tweener = gameObject.AddComponent<LinearTweener>();
        tweener.speed = 20;
        tweener.autoTarget = () =>
        {
            return gridTransform.Target;
        };
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Direction.right);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Grow();
        }
    }





    bool Move(Direction dir)
    {
        return gridTransform.Move(dir);
    }

    void Grow()
    {
        var tailpiece = Player_Tail.Create(gridTransform, gridTransform.CurrentNode);
        tail.Add(tailpiece);
    }
}
