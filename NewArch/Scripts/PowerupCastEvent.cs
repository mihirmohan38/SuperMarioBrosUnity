
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerupCastEvent", menuName = "ScriptableObjects/PowerupCastEvent", order = 7)]

public class PowerupCastEvent : ScriptableObject
{
    private readonly List<PowerupCastEventListener> eventListeners = 
        new List<PowerupCastEventListener>();

    public void Raise(KeyCode p)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(p);
    }

    public void RegisterListener(PowerupCastEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(PowerupCastEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}