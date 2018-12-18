using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventCollider : EventCaller{
    public bool Repeatable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!Repeatable)
                GetComponent<Collider>().enabled = false;
            //Debug.Log(gameObject.name + "was triggered");
            if (EventToCall != null)
                EventToCall.Call();
        }
    }
}
