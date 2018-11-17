using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


//Class Created by RJ
/// <summary>
/// Custom Event System:
///     The custom even system, is responcible for broadcasting events to the game engine.
///     This pattern allows the state of the game engine to be updated when the receiver of the messages are not known.
///     For example, an input controller class may be responcible for getting input from all key presses, and gamepad inputs.
///         The input controller will broadcast all of it's inputs into the event system.
///         The character controller class will be an event handler, and register itself with the event system.
///         Because it's register on the same channel as the input controller, it will receive all events from the input controller.
/// </summary>
public class CEventSystem : MonoBehaviourSingleton
{
    /// <summary>
    /// Sum up the number of event handlers currently in the system.
    /// </summary>
    public static int Count
    {
        get
        {
            var output = 0;
            var dic = GetInstance<CEventSystem>().eventObjects;
            foreach(var subdic in dic)
            {
                foreach(var list in subdic.Value)
                {
                    foreach(var eh in list.Value)
                    {
                        output += 1;
                    }
                }
            }
            return output;
        }
    }

    /// <summary>
    /// The datastructure containing the events in the event system
    /// </summary>
    private Dictionary
        <Enum,
        Dictionary
            <Enum,
            List<ICEventHandler>>>
                eventObjects = new Dictionary<Enum, Dictionary<Enum, List<ICEventHandler>>>();

    /// <summary>
    /// Search the event system for the requested list
    /// </summary>
    /// <param name="category"></param>
    /// <param name="subcategory"></param>
    /// <returns></returns>
    private static List<ICEventHandler> GetHandlerList(Enum category, Enum subcategory, bool create = false)
    {
        var eventObjects = GetInstance<CEventSystem>().eventObjects;
        if(!eventObjects.ContainsKey(category))
        {
            if(!create)
                return null;
            eventObjects[category] = new Dictionary<Enum, List<ICEventHandler>>();
        }
        var cat = eventObjects[category];
        if(!cat.ContainsKey(subcategory))
        {
            if(!create)
                return null;
            cat[subcategory] = new List<ICEventHandler>();
        }
        return cat[subcategory];
    }

    /// <summary>
    /// Add a new event handler to the system
    /// Once the handler is added, it will recieve all events which are boradcasted on the system.
    /// If the object has already been registered in this list, this class will trhow an error.
    /// </summary>
    /// <param name="catagory"></param>
    /// <param name="subcatagory"></param>
    /// <param name="eventHandler"></param>
    public static void AddEventHandler(Enum catagory, Enum subcatagory, ICEventHandler eventHandler)
    {
        var handlerList = GetHandlerList(catagory, subcatagory, true);
        if(handlerList.Contains(eventHandler))
        {
            throw new System.Exception("The event handler is already in this catagory in the event system");
        }
        handlerList.Add(eventHandler);
    }

    /// <summary>
    /// Remove an event handler from the system.
    /// It's important to make sure all event handler are removed when they are disposed of.
    /// Event handlers left in the system, will cause memory leaks, or errors.
    /// </summary>
    /// <param name="catagory"></param>
    /// <param name="subcatagory"></param>
    /// <param name="eventHandler"></param>
    public static void RemoveEventHandler(Enum catagory, Enum subcatagory, ICEventHandler eventHandler)
    {
        var handlerList = GetHandlerList(catagory, subcatagory);
        if(handlerList != null)
            handlerList.Remove(eventHandler);
    }

    /// <summary>
    /// Broadcast an event into the event system.
    /// </summary>
    /// <param name="catagory"></param>
    /// <param name="subcatagory"></param>
    /// <param name="e"></param>
    public static void BroadcastEvent(Enum catagory, Enum subcatagory, CEvent e)
    {
        var handlerList = GetHandlerList(catagory, subcatagory);
        if(handlerList != null)
            foreach(var handler in handlerList)
                handler.AcceptEvent(e);
    }
}

/// <summary>
/// Inherit from this class to create a new event system event.
/// This should be done in the class which needs to interface with the event system (usually the handler)
/// </summary>
public abstract class CEvent
{

}

/// <summary>
/// Implement this interface on your class to allow it to be added to the event system.
/// </summary>
public interface ICEventHandler
{
    void AcceptEvent(CEvent e);
}

public enum EventChannel
{
    none,
    input,
    gameState
}

public enum EventSubChannel
{
    none,
    player1,
    player2,
    player3,
    player4
}