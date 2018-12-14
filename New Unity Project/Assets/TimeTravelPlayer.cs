using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TimeTravelPlayer : TimeTravel
{
    public override bool Rewind()
    {
        if (base.Rewind())
        {
            gameObject.GetComponent<FirstPersonController>().enabled = false;
            return true;
        }
        return false;
    }
    public override bool Clear()
    {
        if(base.Clear())
        {
            gameObject.GetComponent<FirstPersonController>().enabled = true;
            return true;
        }
        return false;
    }
}
