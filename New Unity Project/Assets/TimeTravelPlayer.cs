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
}
