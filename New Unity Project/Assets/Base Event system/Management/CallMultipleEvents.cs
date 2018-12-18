using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMultipleEvents : Event
{
    [SerializeField]
    public List<Event> eventsToCall = new List<Event>();
    public override void Call()
    {
        foreach (Event _Event in eventsToCall)
        {
            _Event.Call();
        }
        next.enabled = true;
        next.Call();
    }
}
