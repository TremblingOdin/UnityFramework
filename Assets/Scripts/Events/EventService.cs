using System;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Attack,
    Click,
    EndLevel,
    GameOver,
    Home,
    Jump,
    MoveRight,
    MoveLeft,
    MoveUp,
    MoveDown,
    Mute,
    Pause,
    RotateLeft,
    RotateRight,
    StartLevel,
    StartSwim,
    StopSwim
};

/// <summary>
/// A singleton entity that handles callbacks for defined events with provided actions
/// </summary>
public class EventService
{
    private Dictionary<EventType, List<Action>> eventHandlers = new Dictionary<EventType, List<Action>>();
    private List<Type> essentials = new List<Type>();

    //Don't mark typr as beforefieldinit
    static EventService() { }

    private EventService() { }

    public static EventService Instance { get; private set; } = new EventService();

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
    /// Only call this method if the type will always be present
    /// </summary>
    /// <param name="t">The type to register as essential</param>
    public void RegisterEssential(Type t)
    {
        essentials.Add(t);
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

    /// <summary>
    /// Checks if the event's reflected type belongs to a singleton or not then removes it if so
    /// </summary>
    public void ClearNonessentialEvents()
    {
        foreach (EventType e in this.eventHandlers.Keys)
        {
            bool essential = false;
            foreach (Action a in this.eventHandlers[e])
            {
                if (essentials.Contains(a.Method.ReflectedType))
                {
                    essential = true;
                }
                else
                {
                    this.eventHandlers[e].Remove(a);
                }
            }

            if (!essential)
                eventHandlers.Remove(e);
        }
    }

    // Don't want paint myself into a corner with the above
    // Avoid calling this
    /// <summary>
    /// Remove the type from the list
    /// </summary>
    /// <param name="t">The type to be removed from essentials</param>
    public void RemoveEssential(Type t)
    {
        if (essentials.Contains(t))
            essentials.Remove(t);
    }
}
