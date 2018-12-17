using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Event : MonoBehaviour {
    public Event next;
    public int calls = 0;
    public virtual void Call()
    {
        calls++;
    }
}
