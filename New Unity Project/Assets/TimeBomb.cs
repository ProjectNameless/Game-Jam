using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    public int range;
    public int speed;
    private float lifeTime;
    public float explosionRadius;
    private void Awake()
    {
        lifeTime = (float)range / speed;
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(speed, 0), ForceMode2D.Impulse);
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            //play explosion animation
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach(Collider2D collider in colliders)
            {
                if (collider.gameObject.GetComponent<AIController>() != null)
                {
                    collider.gameObject.GetComponent<AIController>().ChangeHealth(1000);
                    //play aging animation
                }
            }
            Destroy(gameObject);
        }
    }
}
