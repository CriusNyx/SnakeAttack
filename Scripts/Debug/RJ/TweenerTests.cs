using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenerTests : MonoBehaviour
{
    public int linearState = 0;
    LinearTweener a;
    AsymtoticTweener b;

	void Start ()
    {
        a = new GameObject("Linear").AddComponent<LinearTweener>();
        b = new GameObject("Asymtotic").AddComponent<AsymtoticTweener>();
	}

    void Update()
    {
        //Update a for testing
        if(a.IsDone())
        {
            linearState++;
            linearState = linearState % 4;
            switch(linearState)
            {
                case 0:
                    a.target = Vector2.zero;
                    break;
                case 1:
                    a.target = Vector2.right;
                    break;
                case 2:
                    a.target = Vector2.one;
                    break;
                case 3:
                    a.target = Vector2.up;
                    break;
            }
        }

        //Update b for testing
        if(Time.frameCount % 120 == 0)
        {
            b.target = (Vector2.right * Mathf.Cos(Time.time) + Vector2.up * Mathf.Sin(Time.time)) * 4f;
        }
    }
}