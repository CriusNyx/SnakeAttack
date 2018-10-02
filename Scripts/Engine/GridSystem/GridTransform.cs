using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use this class for grid based movement.
/// Use warp to move a gameObject onto the grid anywhere, or move to to move the gameObject to an adjacent space.
/// Whenever the gameObject moves onto a new grid node, it's event handlers execute.
/// By default it's events will do nothing.
/// </summary>
public class GridTransform : MonoBehaviour
{
    private GridNode currentNode;
    /// <summary>
    /// The current grid node this gameObject is attached to
    /// </summary>
    public GridNode CurrentNode
    {
        get
        {
            return currentNode;
        }
    }

    /// <summary>
    /// The position of the current grid node, in world space
    /// </summary>
    public Vector3 Target
    {
        get
        {
            if(currentNode == null)
                return Vector3.zero;
            else
                return currentNode.transform.position;
        }
    }

    private GridEventHandlers eventHandlers = new GridEventHandlers();
    /// <summary>
    /// Override the events attached to this transform.
    /// By default, they are empty
    /// Throws an exception on a null assignment
    /// </summary>
    public GridEventHandlers Events
    {
        get
        {
            return eventHandlers;
        }
        set
        {
            if(value == null)
                throw new System.Exception("Events cannot be assigned to null");
            eventHandlers = value;
        }
    }

    /// <summary>
    /// Warp this gameObject to a new grid space, ignoring collision
    /// </summary>
    /// <param name="gridNode"></param>
    public void Warp(GridNode gridNode)
    {
        if(currentNode != null)
        {
            currentNode.RemoveTransform(this);
        }
        this.currentNode = gridNode;
        currentNode.AddTransform(this);
    }

    /// <summary>
    /// Move this gameObject to a new grid space
    /// Returns true is the gameObject is able to complete the move
    /// </summary>
    /// <param name="gridNode"></param>
    /// <returns></returns>
    public bool MoveTo(GridNode gridNode)
    {
        if(currentNode == null || currentNode.IsAdjacent(gridNode))
        {
            if(currentNode != null)
                currentNode.RemoveTransform(this);
            currentNode = gridNode;
            gridNode.AddTransform(this);
            return true;
        }
        return false;
    }
}