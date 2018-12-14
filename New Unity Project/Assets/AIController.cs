using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Vector3[] waypoints;
    public int index;
    public float speed;
    void Update()
    {
        transform.LookAt(waypoints[index]);
        if (transform.position.normalized != waypoints[index].normalized)
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }
        else
        {
            index++;
            if (index >= waypoints.Length)
                index = 0;
        }

    }
}
