using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    private void Update()
    {
        transform.position = target.position + offset;
    }
}
