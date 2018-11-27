using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour, ICEventHandler
{
    int enemyCount = 0;

    void Awake() {
        CEventSystem.AddEventHandler(EventChannel.gameState, EventSubChannel.none, this);
	}

    public void AcceptEvent(CEvent e)
    {
        if(e is EnemySpawnEvent)
        {
            enemyCount++;
        }
        if(e is EnemyDestroyedEvent)
        {
            enemyCount--;
            if(enemyCount == 0)
            {
                Debug.Log("Level Complete");
            }
        }
    }

    public class EnemySpawnEvent : CEvent
    {

    }

    public class EnemyDestroyedEvent : CEvent
    {

    }
}