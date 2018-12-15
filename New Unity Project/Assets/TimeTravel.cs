using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    protected List<TimeStamp> stamps = new List<TimeStamp>();
    public int maximumStamps;
    // Update is called once per frame
    void Update()
    {
        if (!Rewind())
            if (!Clear())
                Record();
    }
    public virtual bool Rewind()
    {
        if (Input.GetKey(KeyCode.Space) && stamps.Count > 0)
        {
            stamps[0].Apply(gameObject);
            stamps.RemoveAt(0);
            return true;
        }
        return false;
    }
    public virtual bool Clear()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            stamps.Clear();
            return true;
        }
        else
            return false;
    }
    public virtual void Record()
    {
        if (stamps.Count > maximumStamps)
            stamps.RemoveAt(stamps.Count - 1);
        stamps.Insert(0, new TimeStamp(transform.position, transform.rotation));
    }
}
