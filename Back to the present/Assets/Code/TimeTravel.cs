using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{
    protected List<TimeStamp> stamps = new List<TimeStamp>();
    public bool rewinding;
    public int maximumStamps;
    // Update is called once per frame
    void Update()
    {
        if (!rewinding)
            Record();
    }
    public virtual IEnumerator Rewind()
    {
        while (stamps.Count > 0)
        {
            rewinding = true;
            if (stamps[0] != null && gameObject != null)
            stamps[0].Apply(gameObject);
            stamps.RemoveAt(0);
            yield return null;
        }
        Clear();
    }
    public virtual void Clear()
    {
        stamps.Clear();
        rewinding = false;
    }
    public virtual void Record()
    {
        if (stamps.Count > maximumStamps)
            stamps.RemoveAt(stamps.Count - 1);
        stamps.Insert(0, new TimeStamp(transform.position, transform.rotation));
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        stamps.Clear();
    }
}
