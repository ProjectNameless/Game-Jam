using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public new GameObject camera;
    public int speed;
    public int currentHealth;
    public int maxHealth;
    public float killTime;
    public float startingKillTime;
    public GameObject marker;
    // Update is called once per frame
    private void Start()
    {
        currentHealth = maxHealth;
        GameObject.FindGameObjectWithTag("Health Slider").GetComponent<Slider>().maxValue = maxHealth;
    }
    void Update()
    {
        //camera.transform.position = new Vector3(transform.position.x, transform.position.y, -25);
        //faceDirection(camera.GetComponent<Camera>().ViewportToWorldPoint(Input.mousePosition));
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0));
        faceDirectionMouse();
    }
    public TimeTravel[] changeHealth(int amt)
    {
        currentHealth += amt;
        GameObject.FindGameObjectWithTag("Health Slider").GetComponent<Slider>().value = currentHealth;
        GameObject.FindGameObjectWithTag("Health Text").GetComponent<Text>().text = "Health: " + currentHealth;
        if (currentHealth <= 0)
        {
            TimeTravel[] travelers = FindObjectsOfType<TimeTravel>();
            foreach (TimeTravel traveler in travelers)
                StartCoroutine(traveler.Rewind());
            return travelers;
        }
        return null;
    }
    public void changeHealth(int amt, AIController killer)
    {
        TimeTravel[] travelers = changeHealth(amt);
        if (currentHealth <= 0)
        {
            StartCoroutine(KillTimer(killer, travelers));
        }
    }
    public void faceDirectionMouse()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
    IEnumerator KillTimer(AIController killer, TimeTravel[] travelers)
    {
        killTime = startingKillTime;
        bool stillRewinding = true;
        while (stillRewinding)
        {
            foreach(TimeTravel tt in travelers)
            {
                stillRewinding = tt.rewinding;
                yield return null;
            }
        }
        Instantiate(marker).GetComponent<FollowGameObject>().target = killer.gameObject.transform;
        while (killTime > 0)
        {
            killTime -= Time.deltaTime;
            yield return null;
        }
        if(killer.gameObject.GetComponent<enemyHealth>().health > 0)
        {
            //game over
        }
    }
}
