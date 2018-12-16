using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;
    public int damage;
    public float decayTime;
    private void Awake()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(speed, 0), ForceMode2D.Impulse);
    }
    private void Update()
    {
        decayTime -= Time.deltaTime;
        if (decayTime <= 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
            collision.gameObject.GetComponent<PlayerController>().changeHealth(damage);
        if (collision.gameObject.tag.Equals("Player"))
        Destroy(gameObject);
    }
}
