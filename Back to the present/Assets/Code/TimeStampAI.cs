using System.Collections;
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
        target.transform.position = position;
        AIController controller = target.GetComponent<AIController>();
        controller.gunBarrel.transform.rotation = rotation;
        if (controller.index != waypointIndex)
        {
            controller.index = waypointIndex;
        }
        if (controller.isPlayerSpotted == true && playerSpotted == false)
        {
            controller.isPlayerSpotted = false;
            controller.StopCoroutine(controller.tracking);
        }
    }
}
