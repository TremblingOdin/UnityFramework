using System;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Click,
    EndLevel,
    GameOver,
    Home,
    Mute,
    Pause,
    StartLevel
};

/// <summary>
/// A singleton entity that handles callbacks for defined events with provided actions
/// </summary>
public class EventService
{
    private static readonly EventService instance = new EventService();
    private Dictionary<EventType, List<Action>> eventHandlers = new Dictionary<EventType, List<Action>>();

    //Don't mark typr as beforefieldinit
    static EventService() { }

    private EventService() { }

    public static EventService Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// Adds the action or function to be called when given event occurs.
    /// </summary>
    /// <param name="e">The event that should trigger the function call.</param>
    /// <param name="a">The function to be called when the event occurs</param>
    public void RegisterEventHandler(EventType e, Action a)
    {
        if(eventHandlers.ContainsKey(e) == false)
        {
            eventHandlers.Add(e, new List<Action>());
        }

        eventHandlers[e].Add(a);
    }

    /// <summary>
    /// Calls all registered functions for the given event. This function should be used to trigger the given event.
    /// </summary>
    /// <param name="x"></param>
    public void HandleEvents(EventType x)
    {
        if(eventHandlers.ContainsKey(x) == false)
        {
            Debug.LogWarning("[EventService] Cannot handle requested event: " + x);
        } else
        {
            foreach (Action a in eventHandlers[x])
            {
                a();
            }
        }
    }

    /// <summary>
    /// Checks if the given event type has been registered
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public bool HasEvents(EventType x)
    {
        return eventHandlers.ContainsKey(x);
    }

    /// <summary>
    /// Clear ALL registered events and their handlers.
    /// </summary>
    public void ClearAllEvents()
    {
        eventHandlers.Clear();
    }

    /// <summary>
    /// Clears all event handlers for the provided event.
    /// </summary>
    /// <param name="x">The event used to determine which handlers should be removed.</param>
    public void ClearEvents(EventType x)
    {
        eventHandlers.Remove(x);
    }
}
