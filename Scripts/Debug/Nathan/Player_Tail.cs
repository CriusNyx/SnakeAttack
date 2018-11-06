using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tail : MonoBehaviour {

    GridTransform gridTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private GridTransform leader;
        
    public static Player_Tail Create(GridTransform leader, GridNode position)
    {
        return GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Player_Tail>().Init(leader, position);
    }
    
    private Player_Tail Init(GridTransform leader, GridNode position)
    {
        gridTransform = gameObject.AddComponent<GridTransform>();
        gridTransform.Warp(position);
        transform.position = gridTransform.Target;
        this.leader = leader;
        return this;
    }

}
