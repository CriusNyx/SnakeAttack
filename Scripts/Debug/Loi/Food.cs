using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{

    //Food Prefab
    public GameObject foodPrefab;

    // Borders
    public Transform border_top;
    public Transform border_bottom;
    public Transform border_left;
    public Transform border_right;

    //initialization
    void Start()
    {
        InvokeRepeating("Spawn", 3, 4);
    }
    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(border_left.position.x,
                                  border_right.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(border_bottom.position.y,
                                  border_top.position.y);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }
}