using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestClass : MonoBehaviour
{
    Direction currentDirection = Direction.up;
    GameObject graphic;
    LinearTweener movementTweener;
    GridTransform gridTrasnform;
    List<GameObject> tail = new List<GameObject>();
    int targetCount = 20;

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
            GridNode currentNode = gridTrasnform.CurrentNode;
            if(gridTrasnform.MoveTo(currentNode.GetFromDirection(currentDirection)))
            {
                foreach(var tailPiece in tail)
                {
                    GridTransform g = tailPiece.GetComponent<GridTransform>();
                    GridNode temp = g.CurrentNode;
                    g.MoveTo(currentNode);
                    currentNode = temp;
                }
                if(tail.Count < targetCount)
                {
                    GameObject newPeice = CreateTailPiece();
                    tail.Add(newPeice);
                    newPeice.GetComponent<GridTransform>().Warp(currentNode);
                    newPeice.transform.position = newPeice.GetComponent<GridTransform>().Target;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentDirection != Direction.right)
                currentDirection = Direction.left;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentDirection != Direction.left)
                currentDirection = Direction.right;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(currentDirection != Direction.down)
                currentDirection = Direction.up;   
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentDirection != Direction.up)
                currentDirection = Direction.down;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            targetCount += 20;
        }
    }
    private GameObject CreateTailPiece()
    {
        GameObject output = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var gTransform = output.AddComponent<GridTransform>();
        gTransform.Events = new GridEventHandlers(OnCollision: KillMe);
        var tweener = output.AddComponent<LinearTweener>();
        tweener.autoTarget = () => gTransform.Target;
        tweener.speed = this.movementTweener.speed;
        return output;
    }

    private void KillMe(GridTransform other)
    {
        if(other.GetComponent<PlayerTestClass>() != null)
        {
            Destroy(gameObject);
            foreach(var t in tail)
            {
                Destroy(t);
            }
        }
    }
}