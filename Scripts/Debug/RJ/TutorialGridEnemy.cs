using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGridEnemy : MonoBehaviour {

    GridTransform gridTransform;

    void Awake () {
        gridTransform = gameObject.AddComponent<GridTransform>();
        //This created a new GridEventHandlers class, and assigned the optional
        //parameter OnCollision to equal the method OnGridCollision in this class
        gridTransform.Events = new GridEventHandlers(OnCollision: OnGridCollision);
	}

	void Update () {

	}

    void OnGridCollision(GridTransform other)
    {
        //Determine if the thing I collided with is the player
        if(other.GetComponent<TutorialGridEntity>() != null)
        {
            //If it is, destroy my gameObject
            Destroy(gameObject);
        }
    }
}