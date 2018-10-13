using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTut : MonoBehaviour {

    GridTransform gridTransform;

	void Awake () {
        gridTransform = gameObject.AddComponent<GridTransform>();
        //This create a new GridEventHandlers class, and assigned the optional
        //parameter OnCollision to equal the method OnGridCollision in this class
        gridTransform.Warp(GridSystem.GetNode(20, 20));
        gridTransform.Events = new GridEventHandlers(OnCollision: OnGridCollision);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGridCollision(GridTransform other)
    {
        //Determine if the thing I collided with is the player
        if(other.GetComponent<EntityTut>() != null)
        {
            //If it is, destory my gameObject
            Destroy(gameObject);
        }
    }
}
