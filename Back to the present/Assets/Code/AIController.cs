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
    public bool onPatrol;
    public int minRange;
    public int maxRange;
    public Coroutine tracking;
    public int fireRate;
    public weaponType gunType;
    public int numOfRoundsToFire;
    public GameObject shotgunBulletPrefab;
    public GameObject SMGBulletPrefab;
    public GameObject pistolBulletPrefab;
    public float currentTimer;
    public float rotateSpeed;
    public int health;
    public GameObject gunBarrel;
    Animator anim;
    public AudioSource gunShot;
    public AudioClip SMGround;
    public AudioClip ShotgunRound;
    public AudioClip PistolRound;

    public TimeTravelAI AiTimeTravel;
    public bool isPlayerSpotted = false;
    public Vector3 Target;
    private void Start()
    {
        List<Vector3> waypointsToAdd = new List<Vector3>();
        foreach (GameObject go in Waypoints)
        {
            waypointsToAdd.Add(go.transform.position);
        }
        waypoints = waypointsToAdd.ToArray();
        anim = GetComponent<Animator>();
        Target = waypoints[index];
    }
    void Update()
    {
        if (health > 0)
        {
            if (!isPlayerSpotted)
            {
                if (tracking != null)
                    StopCoroutine(tracking);
                if (CloseEnough(transform.position, waypoints[index], .1f))
                {
                    index++;
                    if (index >= waypoints.Length)
                    {
                        index = 0;
                        if (!onPatrol)
                            speed = 0;
                    }
                    Target = waypoints[index];
                }
                if (FaceDirection(Target))
                {
                    transform.position = transform.position - ((transform.position - Target).normalized * speed * Time.deltaTime);
                }
            }
        }
    }
    public void SpottedPlayer()
    {
        if (!isPlayerSpotted)
        {
            isPlayerSpotted = true;
            tracking = StartCoroutine(TrackPlayer());
        }
    }
    public void ChangeHealth(int amt, bool deathByTimeBomb)
    {
        health += amt;
        anim.SetInteger("Health", health);
        if (deathByTimeBomb)
        {
            anim.SetInteger("Health", int.MinValue);
            anim.SetTrigger("TimeBombDeath");
            Destroy(gunBarrel);
            Destroy(this);
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
    public bool FaceDirection(Vector3 target)
    {
        Vector3 Direction = target - gunBarrel.transform.position;
        gunBarrel.transform.right = gunBarrel.transform.right.normalized - ((gunBarrel.transform.right - Direction) * speed * Time.deltaTime);
        float rotz = gunBarrel.transform.rotation.eulerAngles.z;
        //Debug.Log(rotz);
        if (rotz < 45 || rotz > 315)
        {
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
            anim.SetBool("Up", false);
        }
        else if (rotz > 45 && rotz < 115)
        {
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("Up", true);
        }
        else if (rotz > 115 && rotz < 205)
        {
            anim.SetBool("Right", false);
            anim.SetBool("Left", true);
            anim.SetBool("Up", false);
        }
        else if (rotz > 205 && rotz < 315)
        {
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("Up", false);
        }
        return true;
    }
    public IEnumerator TrackPlayer()
    {
        currentTimer = fireRate;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        while (true)
        {
            //Debug.Log("Running");
            while (!CloseEnough(transform.position, Target, minRange))
            {
                //transform.position = Vector3.Lerp(startPos, player.transform.position, (Time.time - startTime) * speed / totalDistance);
                transform.position = transform.position - ((transform.position - Target).normalized * speed * Time.deltaTime);
                FaceDirection(player.transform.position);
                //shoot();
                yield return null;
            }
            while (TooClose(transform.position, Target, maxRange))
            {
                //transform.position = Vector3.Lerp(startPos, startPos + (transform.position - player.transform.position), (Time.time - startTime) * speed / totalDistance);
                transform.position = transform.position + ((transform.position - Target).normalized * speed * Time.deltaTime);
                FaceDirection(player.transform.position);
                //shoot();
                yield return null;
            }
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {

                if (gunType == weaponType.Shotgun)
                {
                    anim.SetTrigger("Shoot");
                    gunShot.clip = ShotgunRound;
                    gunShot.Play();
                    Instantiate(shotgunBulletPrefab, gunBarrel.transform.position, gunBarrel.transform.rotation);
                    currentTimer = fireRate;
                }
                else if (gunType == weaponType.SMG)
                {
                    for (int i = 0; i < numOfRoundsToFire; i++)
                    {
                        anim.SetTrigger("Shoot");
                        gunShot.clip = SMGround;
                        gunShot.Play();
                        Bullet bullet = Instantiate(SMGBulletPrefab, gunBarrel.transform.position, gunBarrel.transform.rotation).GetComponent<Bullet>();
                        bullet.init(this);
                        yield return new WaitForSeconds(.1f);
                    }
                    currentTimer = fireRate;
                }
                else if (gunType == weaponType.Pistol)
                {
                    anim.SetTrigger("Shoot");
                    gunShot.clip = PistolRound;
                    gunShot.Play();
                    Instantiate(pistolBulletPrefab, gunBarrel.transform.position, gunBarrel.transform.rotation);
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
        SMG,
        Pistol
    }
}