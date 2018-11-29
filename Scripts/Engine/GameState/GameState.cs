using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour, ICEventHandler
{
    int enemyCount = 0;
    public int snakeGrowCount = 5;
    public static int SnakeGrowCount
    {
        get
        {
            return FindObjectOfType<GameState>().snakeGrowCount;
        }
    }

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