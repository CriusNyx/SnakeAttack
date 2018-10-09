using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Inherit from this class to create a new singleton type
/// </summary>
public class MonoBehaviourSingleton : MonoBehaviour
{
    private static Dictionary<Type, object> instances = new Dictionary<Type, object>();
    protected static T GetInstance<T>()
        where T : MonoBehaviourSingleton
    {
        Type type = typeof(T);
        if(!instances.ContainsKey(type) || instances[type] == null)
        {
            instances[type] = FindObjectOfType<T>();
        }
        if(!instances.ContainsKey(type) || instances[type] == null)
        {
            instances[type] = new GameObject(type.Name).AddComponent<T>();
        }
        return (T)instances[type];
    }
}