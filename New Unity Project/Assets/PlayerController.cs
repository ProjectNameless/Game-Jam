using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public new GameObject camera;
    public int speed;
    public int health;
    // Update is called once per frame
    void Update()
    {
        //camera.transform.position = new Vector3(transform.position.x, transform.position.y, -25);
        //faceDirection(camera.GetComponent<Camera>().ViewportToWorldPoint(Input.mousePosition));
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0));
    }
    public void changeHealth(int amt)
    {
        health += amt;
        if (health <= 0)
        {
            TimeTravelPlayer timetravel = GetComponent<TimeTravelPlayer>();
            StartCoroutine(timetravel.Rewind());
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                StartCoroutine(enemy.GetComponent<TimeTravelAI>().Rewind());
            }
        }
    }

    public void faceDirection(Vector3 worldPosition)
    {
        Vector3 Direction = worldPosition - transform.position;
        transform.right = new Vector2(Direction.x, Direction.y);
    }
}
