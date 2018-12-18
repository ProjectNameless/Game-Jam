using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelPlayer : TimeTravel
{
    public override IEnumerator Rewind()
    {
        gameObject.GetComponent<PlayerController>();
        yield return base.Rewind();
    }
    public override void Clear()
    {
        gameObject.GetComponent<PlayerController>();
        base.Clear();
    }
    public override void Record()
    {
        stamps.Insert(0, new TimeStampPlayer(transform.position, transform.rotation, GetComponent<PlayerController>().currentHealth));
    }
}
