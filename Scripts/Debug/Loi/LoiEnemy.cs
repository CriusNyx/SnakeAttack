using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStateEvents;
public class LoiEnemy : MonoBehaviour {
    GridTransform gridTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGridCollision(GridTransform other)
    {
        CEventSystem.BroadcastEvent(EventChannel.gameState,EventSubChannel.none, new GameOverEvent());
        Destroy(gameObject);
        //CEventSystem.BroadcastEvent(EventChannel.gameState, EventSubChannel.none, new GrowEvent(1));
    }


    private void Awake()
    {   //generic method
        gridTransform = gameObject.GetComponent<GridTransform>();
        //=> is a lambda
        gridTransform.Events = new GridEventHandlers(OnCollision: (x) => OnGridCollision(x));

    }



}
