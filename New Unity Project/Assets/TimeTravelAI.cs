using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelAI : TimeTravel
{
    AIController controller;
    void Start()
    {
        controller = GetComponent<AIController>();
    }
    public override void Record()
    {
        if (stamps.Count > maximumStamps)
            stamps.RemoveAt(stamps.Count - 1);
        stamps.Insert(0, new TimeStampAI(transform.position, transform.rotation, controller.index));
    }
    public override void Clear()
    {
        base.Clear();
        controller.enabled = true;
        controller.refreshPath();
    }
    public override IEnumerator Rewind()
    {
        controller.enabled = false;
        yield return base.Rewind();
    }
}
