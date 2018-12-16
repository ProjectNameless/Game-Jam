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
    }
    public void changeHealth(int amt)
    {
        currentHealth += amt;
        GameObject.FindGameObjectWithTag("Health Slider").GetComponent<Slider>().value = currentHealth;
        GameObject.FindGameObjectWithTag("Health Text").GetComponent<Text>().text = "Health: " + currentHealth;
        if (currentHealth <= 0)
        {
            TimeTravel[] travelers = FindObjectsOfType<TimeTravel>();
            foreach (TimeTravel traveler in travelers)
                StartCoroutine(traveler.Rewind());
        }
    }

    public void faceDirection(Vector3 worldPosition)
    {
        Vector3 Direction = worldPosition - transform.position;
        transform.right = new Vector2(Direction.x, Direction.y);
    }
}
