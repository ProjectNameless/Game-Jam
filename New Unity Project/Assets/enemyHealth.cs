using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{

    public int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die ()
    {
        //play die animation or whatever
        Destroy(this);
    }

    public void takeDamage (int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Die();
        }
    }
}
