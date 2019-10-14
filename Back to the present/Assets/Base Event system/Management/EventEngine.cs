using System;
using System.Collections.Generic;
using UnityEngine;

public class EventEngine : MonoBehaviour {

    public static EventEngine instance;
    public List<GameObject> events = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public void Call(string name)
    {
        GameObject eventToCall = Array.Find(events.ToArray(), searchevent => searchevent.name == name);
        if (eventToCall != null)
            eventToCall.GetComponent<Event>().Call();
        else
            Debug.LogWarning("Event with the name: " + name + " does not exist!");
    }
}
