using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridNode : MonoBehaviour, IEnumerable<GridNode>
{
    public GridNode left, right, top, bottom;

    private List<GridTransform> connectedTransforms = new List<GridTransform>();

    /// <summary>
    /// Gets a grid space from a direction, up, down, left of right
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="recoverFromError">if true, and the direction can't be recognized, then it will return null</param>
    /// <returns></returns>
    public GridNode GetFromDirection(Vector2 direction, bool recoverFromError = false)
    {
        if(direction == Vector2.left)
            return left;
        else if(direction == Vector2.right)
            return right;
        else if(direction == Vector2.up)
            return top;
        else if(direction == Vector2.down)
            return bottom;
        else if(recoverFromError)
            return null;
        else
            throw new System.Exception("Unrecognized direction. Directions can only be, left, right, up, or down");
    }

    /// <summary>
    /// Check if this node is adjacent to the other node
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsAdjacent(GridNode other)
    {
        return this.Contains(other);
    }

    /// <summary>
    /// Connect a grid transform to this node, and execute it's events
    /// </summary>
    /// <param name="transform"></param>
    public void AddTransform(GridTransform transform)
    {
        foreach(var t in connectedTransforms)
        {
            t.Events.OnCollision(transform);
        }
        connectedTransforms.Add(transform);
    }

    /// <summary>
    /// Disconnect the grid trasnform from this node, and execute any events
    /// </summary>
    /// <param name="transform"></param>
    public void RemoveTransform(GridTransform transform)
    {
        connectedTransforms.Remove(transform);
    }

    /// <summary>
    /// Returns a list of transforms connected to this one
    /// </summary>
    /// <returns></returns>
    public IEnumerator<GridNode> GetEnumerator()
    {
        if(left != null)
            yield return left;
        if(right != null)
            yield return right;
        if(top != null)
            yield return top;
        if(bottom != null)
            yield return bottom;
    }

    /// <summary>
    /// Returns a list of transforms connected to this one
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        if(left != null)
            yield return left;
        if(right != null)
            yield return right;
        if(top != null)
            yield return top;
        if(bottom != null)
            yield return bottom;
    }
}