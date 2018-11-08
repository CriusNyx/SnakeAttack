using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TutorialGridEnemy : MonoBehaviour
{
    GridTransform gridTransform;
    public GameObject Cube;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         
    void Awake()
    {
        gridTransform = gameObject.AddComponent<GridTransform>();
        //created a new grideventhandlers class and assigne the optional
        // parameter oncollision to equal the method OnGridCollision in
        //this class
        //gridTransform.Warp(GridSystem.GetNode(10, 10));
        gridTransform.Events = new GridEventHandlers(OnCollision: OnGridCollision);
        gridTransform.Warp(GridSystem.GetNode(5, 5));
    }

    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Update()
    {
        //Move the 3d position to the grid positions
       // transform.position = gridTransform.Target;
    }
    void Spawn()
    {
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(Cube, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    void OnGridCollision(GridTransform other)
    {
        //determite if the thing I collided with is the player
        if (other.GetComponent<gridTut>() != null)
        {
            //if it is, destory my gameobject
            Destroy(gameObject);
        }

    }
}
