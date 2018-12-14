using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStamp
{
    public Vector3 position { get; private set; }
    public Quaternion rotation { get; private set; }
    public TimeStamp(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
    public virtual void Apply(GameObject target)
    {
        target.transform.position = position;
        target.transform.rotation = rotation;
    }
}
