using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStampAI : TimeStamp
{
    public int waypointIndex;
    public TimeStampAI(Vector3 position, Quaternion rotation, int index) : base (position, rotation)
    {
        waypointIndex = index;
    }
    public override void Apply(GameObject target)
    {
        base.Apply(target);
        target.GetComponent<AIController>().index = waypointIndex;
    }
}
