using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Used to store event interactions for the Grid System
/// </summary>
public class GridEventHandlers
{
    public readonly Action<GridTransform> OnCollision;
    /// <summary>
    /// Create a new collection of event handlers
    /// </summary>
    /// <param name="OnCollision"></param>
    public GridEventHandlers(Action<GridTransform> OnCollision = null)
    {
        if(OnCollision == null)
            OnCollision = (x) => { };
        this.OnCollision = OnCollision;
    }
}