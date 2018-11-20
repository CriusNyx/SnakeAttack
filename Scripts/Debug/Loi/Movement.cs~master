using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float accel = -9.8f;
    float velocity;
    

    // Use this for initialization
    void Start()
    {
      var quad =  GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            input.y += 1;
       
   }
        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1;

        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y -= 1;

        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;

        }


        input = input.normalized;
        transform.position = transform.position + input*Time.deltaTime*20;

        velocity = velocity + accel * Time.deltaTime;
        transform.position += Vector3.up * Time.deltaTime * velocity;
        if (Input.GetKeyDown(KeyCode.Space)) {
            velocity = 10f;
        }
        
    }

}
