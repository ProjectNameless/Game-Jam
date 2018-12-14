using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelPlayer : TimeTravel
{
    public override bool Rewind()
    {
        if (base.Rewind())
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
            return true;
        }
        return false;
    }
    public override bool Clear()
    {
        if(base.Clear())
        {
            gameObject.GetComponent<PlayerController>().enabled = true;
            return true;
        }
        return false;
    }
}
