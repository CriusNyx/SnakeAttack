using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;

    private void Awake()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        GridNode node = GridSystem.GetNode(x, y);

        GameObject newObject = Instantiate(prefab);
        GridTransform gridTransform = newObject.GetComponent<GridTransform>();
        if(gridTransform != null && node != null)
        {
            gridTransform.Warp(node);
        }
    }
}