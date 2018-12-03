using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour, ICEventHandler
{
    int enemyCount = 0;
    public int snakeGrowCount = 5;
    public string nextLevel = "";
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
                AllEnemies();
            }
        }
        if(e is WinLevelEvent)
        {
            CompleteLevel();
        }
    }

    private void AllEnemies()
    {
        Chest.EnableChest();
    }

    private void CompleteLevel()
    {
        GameObject levelCompleteText = new GameObject("Level_Complete_Text");
        TextMesh text = levelCompleteText.AddComponent<TextMesh>();
        text.fontSize = 100;
        text.characterSize = 0.02f;
        text.anchor = TextAnchor.MiddleCenter;
        text.text = "Level Complete";
        text.alignment = TextAlignment.Center;
        text.transform.SetParent(GameObject.Find("Main Camera").transform, false);
        text.transform.localPosition = Vector3.forward * 1f;
        SoundController.PlaySound(Player.Instance.transform.position, "Resources/Sounds/VictorySound");

        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        float time = Time.time + 1f;
        while(Time.time < time)
        {
            yield return null;
        }
        SceneManager.LoadScene(nextLevel);
    }

    public class EnemySpawnEvent : CEvent
    {

    }

    public class EnemyDestroyedEvent : CEvent
    {

    }

    public class WinLevelEvent : CEvent
    {

    }
}