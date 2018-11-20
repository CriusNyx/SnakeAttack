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
    /// Returns true if the character is able to move to the specified node.
    /// </summary>
    /// <param name="gridNode"></param>
    /// <returns></returns>
    public bool CanMoveTo(GridNode gridNode)
    {
        return currentNode != null && currentNode.IsAdjacent(gridNode);
    }

    /// <summary>
    /// Returns true if the character is able to move in the specified direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool CanMoveTo(Direction direction)
    {
        if(currentNode == null)
            return false;
        GridNode next = CurrentNode.GetFromDirection(direction);
        return CanMoveTo(next);
    }

    /// <summary>
    /// Move this gameObject to a new grid space
    /// Returns true is the gameObject is able to complete the move
    /// </summary>
    /// <param name="gridNode"></param>
    /// <returns></returns>
    public bool MoveTo(GridNode gridNode)
    {
        if(gridNode == null)
            return false;
        if(CanMoveTo(gridNode))
        {
            if(currentNode != null)
                currentNode.RemoveTransform(this);
            currentNode = gridNode;
            gridNode.AddTransform(this);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Move this gameObject to the adjacent grid space in the specified direction
    /// Retrusn true if the move was successful
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool Move(Direction direction)
    {
        if(CurrentNode == null)
        {
            throw new System.Exception("Cannot preform move because the gridTransform is not currently attached to a grid node.\nUse gridTransform.Warp(GridSystem.GetNode(x, y)) to move the entity to the grid");
        }
        GridNode next = CurrentNode.GetFromDirection(direction);
        return MoveTo(next);
    }

    private void OnDestroy()
    {
        currentNode.RemoveTransform(this);
    }
}