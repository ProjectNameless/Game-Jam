using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Vector3[] waypoints;
    public int index;
    public float speed;
    private float startTime;
    private float totalDistance;
    private Vector3 startPos;
    public bool onPatrol;
    void Update()
    {
        if (!CloseEnough(transform.position, waypoints[index], .1f))
        {
            transform.position = Vector3.Lerp(startPos, waypoints[index], (Time.time - startTime) * speed / totalDistance);
        }
        else
        {
            index++;
            if (index >= waypoints.Length)
            {
                index = 0;
                if (!onPatrol)
                    speed = 0;
            }
            refreshPath();
        }
    }
    bool CloseEnough(Vector3 a, Vector3 b, float maxDifference)
    {
        if (Vector3.Distance(a, b) < maxDifference)
            return true;
        return false;
    }
    public void refreshPath()
    {
        startTime = Time.time;
        totalDistance = Vector3.Distance(transform.position, waypoints[index]);
        startPos = transform.position;
    }
}
