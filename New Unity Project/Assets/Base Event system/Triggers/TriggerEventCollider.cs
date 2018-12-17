using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventCollider : EventCaller{
    public bool Repeatable;
    private void OnTriggerEnter(Collider other)
    {
        if (!Repeatable)
        GetComponent<Collider>().enabled = false;
        //Debug.Log(gameObject.name + "was triggered");
        if (EventToCall != null)
            EventToCall.Call();
    }
}
