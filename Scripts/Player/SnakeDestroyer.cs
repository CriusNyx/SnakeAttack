using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDestroyer : MonoBehaviour
{
    GameObject player;
    IEnumerable<GameObject> tailList;

    /// <summary>
    /// This object is used to destoy a snake.
    /// Pass in the head of the snake, and the tail to destoy
    /// </summary>
    /// <param name="head"></param>
    /// <param name="tailList"></param>
    /// <returns></returns>
    public static SnakeDestroyer Create(GameObject head, IEnumerable<GameObject> tailList)
    {
        return new GameObject("Snake Destroyer").AddComponent<SnakeDestroyer>().Init(head, tailList);
    }

    private SnakeDestroyer Init(GameObject player, IEnumerable<GameObject> tailList)
    {
        this.player = player;
        this.tailList = tailList;
        return this;
    }

    private void Start()
    {
        StartCoroutine(Work());
    }

    private IEnumerator Work()
    {
        Destroy(player);
        yield return null;
        foreach(var go in tailList)
        {
            yield return null;
            Destroy(go);
        }
        Destroy(gameObject);
    }
}