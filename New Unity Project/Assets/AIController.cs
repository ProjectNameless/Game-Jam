using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Patrol route")]
    public GameObject[] Waypoints;
    [HideInInspector]
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
    public int fireRate;
    public weaponType gunType;
    public int numOfRoundsToFire;
    public GameObject shotgunBulletPrefab;
    public GameObject SMGBulletPrefab;
    public float currentTimer;
    public float rotateSpeed;
    public int health;
    private void Start()
    {
        List<Vector3> waypointsToAdd = new List<Vector3>();
        foreach (GameObject go in Waypoints)
        {
            waypointsToAdd.Add(go.transform.position);
        }
        waypoints = waypointsToAdd.ToArray();
    }
    void Update()
    {
        if (!CloseEnough(transform.position, waypoints[index], .1f) && !playerSpotted)
        {
            if (tracking != null)
                StopCoroutine(tracking);
            if(faceDirection(waypoints[index]))
            transform.position = transform.position - ((transform.position - waypoints[index]).normalized * speed * Time.deltaTime);
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
    public void ChangeHealth(int amt)
    {
        health += amt;
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
    public bool faceDirection(Vector3 worldPosition)
    {
        Vector3 Direction = worldPosition - transform.position;
        transform.right = transform.right.normalized - ((transform.right - Direction) * speed * Time.deltaTime);
        return true;
    }
    public IEnumerator trackPlayer()
    {
        currentTimer = fireRate;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        refreshPath(player.transform.position);
        while (true)
        {
            //Debug.Log("Running");
            faceDirection(player.transform.position);
            while (!CloseEnough(transform.position, player.transform.position, minRange))
            {
                //transform.position = Vector3.Lerp(startPos, player.transform.position, (Time.time - startTime) * speed / totalDistance);
                transform.position = transform.position - ((transform.position - player.transform.position).normalized * speed * Time.deltaTime);
                faceDirection(player.transform.position);
                //shoot();
                yield return null;
            }
            refreshPath(player.transform.position);
            while (TooClose(transform.position, player.transform.position, maxRange))
            {
                //transform.position = Vector3.Lerp(startPos, startPos + (transform.position - player.transform.position), (Time.time - startTime) * speed / totalDistance);
                transform.position = transform.position + ((transform.position - player.transform.position).normalized * speed * Time.deltaTime);
                faceDirection(player.transform.position);
                //shoot();
                yield return null;
            }
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                if (gunType == weaponType.Shotgun)
                {
                    Instantiate(shotgunBulletPrefab, transform.position, transform.rotation);
                    currentTimer = fireRate;
                }
                else if (gunType == weaponType.SMG)
                {
                    for (int i = 0; i < numOfRoundsToFire; i++)
                    {
                        Bullet bullet = Instantiate(SMGBulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
                        bullet.init(this);
                        yield return new WaitForSeconds(.1f);
                    }
                    currentTimer = fireRate;
                }
            }
            while (health <= 0)
            {
                yield return null;
            }
            yield return null;
        }
    }
    public enum weaponType
    {
        Shotgun,
        SMG
    }
}
