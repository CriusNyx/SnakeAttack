using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Attach this to a game object.
/// Each frame, this game object will move closer to it's target.
/// </summary>
public class LinearTweener : MonoBehaviour
{
    /// <summary>
    /// The position this game object will move toward
    /// </summary>
    public Vector3 target;
    /// <summary>
    /// The speed at which this will move toward it's target
    /// </summary>
    public float speed = 1f;

    /// <summary>
    /// Assign this to make the component automatically follow something
    /// </summary>
    public Func<Vector3> autoTarget;

    /// <summary>
    /// Returns true if the game object has reached it's target.
    /// </summary>
    /// <returns></returns>
    public bool IsDone()
    {
        return transform.position == target;
    }

    /// <summary>
    /// Applies the movement each frame.
    /// </summary>
    void Update()
    {
        if(autoTarget != null)
            target = autoTarget();
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}