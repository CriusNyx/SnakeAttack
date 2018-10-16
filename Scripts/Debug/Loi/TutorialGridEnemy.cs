using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TutorialGridEnemy : MonoBehaviour
{
    GridTransform gridTransform;

    void Awake()
    {
        gridTransform = gameObject.AddComponent<GridTransform>();
        //created a new grideventhandlers class and assigne the optional
        // parameter oncollision to equal the method OnGridCollision in
        //this class
        gridTransform.Warp(GridSystem.GetNode(10, 10));
        gridTransform.Events = new GridEventHandlers(OnCollision: OnGridCollision);

    }

   
    void Update()
    {

    }

    void OnGridCollision(GridTransform other)
    {
        //determite if the thing I collided with is the player
        if (other.GetComponent<gridTut>() != null)
        {
            //if it is, destory my gameobject
            Destroy(gameObject);
        }
    }
}
