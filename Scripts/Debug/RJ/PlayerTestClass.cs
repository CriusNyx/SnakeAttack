using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestClass : MonoBehaviour
{
    Direction currentDirection = Direction.up;
    GameObject graphic;
    LinearTweener movementTweener;
    GridTransform gridTrasnform;

    private void Start()
    {
        graphic = GameObject.CreatePrimitive(PrimitiveType.Cube);
        graphic.transform.SetParent(transform);

        movementTweener = gameObject.AddComponent<LinearTweener>();
        gridTrasnform = gameObject.AddComponent<GridTransform>();
        gridTrasnform.Warp(GridSystem.GetNode(0, 0));
        transform.position = gridTrasnform.Target;
        movementTweener.target = gridTrasnform.Target;
        movementTweener.speed = 500;
    }

    private void Update()
    {
        movementTweener.target = gridTrasnform.Target;
        if(movementTweener.IsDone())
        {
            gridTrasnform.MoveTo(gridTrasnform.CurrentNode.GetFromDirection(currentDirection));
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentDirection != Direction.right)
                currentDirection = Direction.left;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentDirection != Direction.left)
                currentDirection = Direction.right;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentDirection != Direction.down)
                currentDirection = Direction.up;   
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentDirection != Direction.up)
                currentDirection = Direction.down;
        }
    }
}