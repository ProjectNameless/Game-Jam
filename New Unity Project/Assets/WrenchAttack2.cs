using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchAttack2 : MonoBehaviour
{
    public int damage;
    public float range;
    public float attackDelay;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && attackDelay <= 0f)
        {
            Debug.Log("shooting");

            Shoot();
        }

        if (attackDelay > 0f)
        {
            attackDelay = attackDelay - Time.deltaTime;
        }

    }

    void Shoot()
    {
        Vector3 rayOrigin = new Vector3(player.transform.position.x, player.transform.position.y);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y);

        mousePos = rayOrigin - mousePos;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, mousePos, range, 9);

        if(hit.collider != null)
        {
            if (hit.collider.GetComponent<enemyHealth>() != null)
            {
                hit.collider.GetComponent<enemyHealth>().takeDamage(damage);
            }
        }

    }
}
