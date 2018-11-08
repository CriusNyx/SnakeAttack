 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to a game object to make it move closer to it's target each frame.
/// This type of tweener never reaches it's target. Rather, it moves quickly when it's far away from it's target, and slows down as it gets closer.
/// This kind of tweener is useful for a camera, because the smooth movement helps prevent motion sickness.
/// </summary>
public class AsymtoticTweener : MonoBehaviour
{
    /// <summary>
    /// The target this tweener will move towards
    /// </summary>
    public Vector3 target;
    /// <summary>
    /// This ratio determines how much closer it will be to it's target after each second.
    /// For example, setting this to 0.9 will make it close 90% of the remaining distance each second.
    /// Note: Some error will be introduced in it's trajectory because of variations in the frame rate.
    /// </summary>
    public float movementRatio = 0.95f;

    /// <summary>
    /// Applies the movement each frame.
    /// </summary>
    private void LateUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, movementRatio * Time.deltaTime);
    }
}