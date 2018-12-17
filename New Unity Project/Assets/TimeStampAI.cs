﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStampAI : TimeStamp
{
    public int waypointIndex;
    public bool playerSpotted;
    public TimeStampAI(Vector3 position, Quaternion rotation, int index) : base (position, rotation)
    {
        waypointIndex = index;
    }
    public override void Apply(GameObject target)
    {
        base.Apply(target);
        AIController controller = target.GetComponent<AIController>();
        if (controller.index != waypointIndex)
        {
            controller.index = waypointIndex;
            controller.RefreshPath(controller.waypoints[controller.index]);
        }
        if (controller.playerSpotted == true && playerSpotted == false)
        {
            controller.playerSpotted = false;
            controller.StopCoroutine(controller.tracking);
        }
    }
}
