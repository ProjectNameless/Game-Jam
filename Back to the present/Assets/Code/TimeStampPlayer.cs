using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStampPlayer : TimeStamp
{
    public int health;
    public TimeStampPlayer(Vector3 position, Quaternion rotation, int health) : base(position, rotation)
    {
        this.health = health;
    }
    public override void Apply(GameObject target)
    {
        base.Apply(target);
        target.GetComponent<PlayerController>().SetHealth(health);
    }
}
