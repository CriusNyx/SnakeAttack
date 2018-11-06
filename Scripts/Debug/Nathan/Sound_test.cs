using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Garbage");
            GameObject instance = GameObject.Instantiate(prefab);
            instance.AddComponent<Sound_DestroyWhenDone>(); 
        }
	}
}
