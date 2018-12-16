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
    public bool playerSpotted;
    public int minRange;
    public int maxRange;
    public Coroutine tracking;
    void Update()
    {
        if (!CloseEnough(transform.position, waypoints[index], .1f) && !playerSpotted)
        {
            if (tracking != null)
                StopCoroutine(tracking);
            faceDirection(waypoints[index]);
            transform.position = Vector3.Lerp(startPos, waypoints[index], (Time.time - startTime) * speed / totalDistance);
        }
        else if (!playerSpotted)
        {
            index++;
            if (index >= waypoints.Length)
            {
                index = 0;
                if (!onPatrol)
                    speed = 0;
            }
            refreshPath(waypoints[index]);
        }
    }
    public void spottedPlayer()
    {
        if (!playerSpotted)
        {
            playerSpotted = true;
            tracking = StartCoroutine(trackPlayer());
        }
    }
    bool CloseEnough(Vector3 a, Vector3 b, float maxDistance)
    {
        if (Vector3.Distance(a, b) <= maxDistance)
            return true;
        return false;
    }
    bool TooClose(Vector3 a, Vector3 b, float minDistance)
    {
        if (Vector3.Distance(a, b) < minDistance)
            return true;
        return false;
    }
    public void refreshPath(Vector3 worldPosition)
    {
        startTime = Time.time;
        totalDistance = Vector3.Distance(transform.position, waypoints[index]);
        startPos = transform.position;
    }
    public void faceDirection(Vector3 worldPosition)
    {
        Vector3 Direction = worldPosition - transform.position;
        transform.right = new Vector2(Direction.x, Direction.y);
    }
    private void FaceOppositeDirection(Vector3 worldPosition)
    {
        Vector3 Direction = worldPosition + transform.position;
        transform.right = new Vector2(Direction.x, Direction.y);
    }
    public IEnumerator trackPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        refreshPath(player.transform.position);
        while (true)
        {
            faceDirection(player.transform.position);
            while (!CloseEnough(transform.position, player.transform.position, minRange))
            {
                Debug.Log("heading to player");
                transform.position = Vector3.Lerp(startPos, player.transform.position, (Time.time - startTime) * speed / totalDistance);
                yield return null;
            }
            refreshPath(player.transform.position);
            while (TooClose(transform.position, player.transform.position, maxRange))
            {
                Debug.Log("running from player");
                transform.position = Vector3.Lerp(startPos, startPos + (transform.position-player.transform.position), (Time.time - startTime) * speed / totalDistance);
                yield return null;
            }
            Debug.Log("I caught up to the player");
            yield return null;
        }
    }
}
